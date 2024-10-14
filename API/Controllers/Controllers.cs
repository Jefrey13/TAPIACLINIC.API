using Application.Models;
using Application.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PermissionsController : ControllerBase
    {
        private readonly IPermissionAppService _permissionAppService;

        public PermissionsController(IPermissionAppService permissionAppService)
        {
            _permissionAppService = permissionAppService;
        }

        /// <summary>
        /// Retrieves all the permissions.
        /// </summary>
        /// <returns>A list of PermissionDto.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PermissionDto>>> GetAllPermissions()
        {
            var permissions = await _permissionAppService.GetAllPermissionsAsync();
            return Ok(permissions);
        }

        /// <summary>
        /// Retrieves a specific permission by ID.
        /// </summary>
        /// <param name="id">The ID of the permission to retrieve.</param>
        /// <returns>A PermissionDto.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<PermissionDto>> GetPermissionById(int id)
        {
            var permission = await _permissionAppService.GetPermissionByIdAsync(id);
            if (permission == null)
            {
                return NotFound();
            }
            return Ok(permission);
        }

        /// <summary>
        /// Creates a new permission.
        /// </summary>
        /// <param name="permissionDto">The details of the permission to create.</param>
        /// <returns>The ID of the newly created permission.</returns>
        [HttpPost]
        public async Task<ActionResult<int>> CreatePermission([FromBody] PermissionDto permissionDto)
        {
            var createdPermissionId = await _permissionAppService.CreatePermissionAsync(permissionDto);
            return CreatedAtAction(nameof(GetPermissionById), new { id = createdPermissionId }, createdPermissionId);
        }

        /// <summary>
        /// Updates an existing permission.
        /// </summary>
        /// <param name="id">The ID of the permission to update.</param>
        /// <param name="permissionDto">The updated details of the permission.</param>
        /// <returns>No content if successful.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePermission(int id, [FromBody] PermissionDto permissionDto)
        {
            await _permissionAppService.UpdatePermissionAsync(id, permissionDto);
            return NoContent();
        }

        /// <summary>
        /// Deletes a permission by ID.
        /// </summary>
        /// <param name="id">The ID of the permission to delete.</param>
        /// <returns>No content if successful.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePermission(int id)
        {
            await _permissionAppService.DeletePermissionAsync(id);
            return NoContent();
        }
    }
}