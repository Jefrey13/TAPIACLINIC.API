using Application.Commands.Roles;
using Application.Models.ReponseDtos;
using Application.Models.RequestDtos;
using Application.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Controllers
{
    /// <summary>
    /// API controller for managing roles.
    /// Provides endpoints for creating, updating, deleting, and retrieving roles.
    /// </summary>
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
        /// Retrieves all roles in the system.
        /// </summary>
        /// <returns>A list of RoleDto objects representing all roles.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RoleResponseDto>>> GetAllRoles()
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
        public async Task<ActionResult<RoleResponseDto>> GetRoleById(int id)
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
        /// <param name="command">The command containing role details.</param>
        /// <returns>The ID of the newly created role.</returns>
        [HttpPost]
        public async Task<ActionResult<int>> CreateRole([FromBody] CreateRoleCommand command)
        {
            var createdRoleId = await _roleAppService.CreateRoleAsync(command);
            return CreatedAtAction(nameof(GetRoleById), new { id = createdRoleId }, createdRoleId);
        }

        /// <summary>
        /// Updates an existing role.
        /// </summary>
        /// <param name="id">The ID of the role to update.</param>
        /// <param name="roleDto">The updated role details.</param>
        /// <returns>No content if the update is successful, 400 if the ID mismatch occurs.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRole(int id, [FromBody] RoleRequestDto roleDto)
        {
            var command = new UpdateRoleCommand(id, roleDto);
            await _roleAppService.UpdateRoleAsync(id, command);
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
            await _roleAppService.DeleteRoleAsync(new DeleteRoleCommand(id));
            return NoContent();
        }
    }
}