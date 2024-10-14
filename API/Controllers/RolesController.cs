using Application.Models;
using Application.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly IRoleAppService _roleAppService;

        public RolesController(IRoleAppService roleAppService)
        {
            _roleAppService = roleAppService;
        }

        /// <summary>
        /// Retrieves all the roles in the system.
        /// </summary>
        /// <returns>A list of RoleDto containing details of all roles.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RoleDto>>> GetAllRoles()
        {
            var roles = await _roleAppService.GetAllRolesAsync();
            return Ok(roles);
        }

        /// <summary>
        /// Retrieves a specific role by its ID.
        /// </summary>
        /// <param name="id">The ID of the role to retrieve.</param>
        /// <returns>The RoleDto of the requested role, or 404 if not found.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<RoleDto>> GetRoleById(int id)
        {
            var role = await _roleAppService.GetRoleByIdAsync(id);
            if (role == null)
            {
                return NotFound();
            }
            return Ok(role);
        }

        /// <summary>
        /// Creates a new role.
        /// </summary>
        /// <param name="roleDto">The details of the role to be created.</param>
        /// <returns>The ID of the newly created role.</returns>
        [HttpPost]
        public async Task<ActionResult<int>> CreateRole([FromBody] RoleDto roleDto)
        {
            var createdRoleId = await _roleAppService.CreateRoleAsync(roleDto);
            return CreatedAtAction(nameof(GetRoleById), new { id = createdRoleId }, createdRoleId);
        }

        /// <summary>
        /// Updates an existing role.
        /// </summary>
        /// <param name="id">The ID of the role to update.</param>
        /// <param name="roleDto">The updated details of the role.</param>
        /// <returns>No content if the update is successful, 400 if the ID mismatch occurs.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRole(int id, [FromBody] RoleDto roleDto)
        {
            if (id != roleDto.Id)
            {
                return BadRequest("Role ID in the request does not match the one in the body.");
            }

            await _roleAppService.UpdateRoleAsync(id, roleDto);
            return NoContent();
        }

        /// <summary>
        /// Deletes a role by its ID.
        /// </summary>
        /// <param name="id">The ID of the role to delete.</param>
        /// <returns>No content if the deletion is successful.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRole(int id)
        {
            await _roleAppService.DeleteRoleAsync(id);
            return NoContent();
        }
    }
}