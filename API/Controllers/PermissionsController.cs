using Application.Models;
using Application.Services;
using API.Utils;
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
        public async Task<ActionResult<ApiResponse<IEnumerable<PermissionDto>>>> GetAllPermissions()
        {
            var permissions = await _permissionAppService.GetAllPermissionsAsync();
            var response = new ApiResponse<IEnumerable<PermissionDto>>(true, "Permissions retrieved successfully", permissions, 200);
            return Ok(response);
        }

        /// <summary>
        /// Retrieves a specific permission by ID.
        /// </summary>
        /// <param name="id">The ID of the permission to retrieve.</param>
        /// <returns>A PermissionDto.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<PermissionDto>>> GetPermissionById(int id)
        {
            var permission = await _permissionAppService.GetPermissionByIdAsync(id);
            if (permission == null)
            {
                var errorResponse = new ApiResponse<PermissionDto>(false, "Permission not found", null, 404);
                return NotFound(errorResponse);
            }
            var response = new ApiResponse<PermissionDto>(true, "Permission retrieved successfully", permission, 200);
            return Ok(response);
        }

        /// <summary>
        /// Creates a new permission.
        /// </summary>
        /// <param name="permissionDto">The details of the permission to create.</param>
        /// <returns>The ID of the newly created permission.</returns>
        [HttpPost]
        public async Task<ActionResult<ApiResponse<int>>> CreatePermission([FromBody] PermissionDto permissionDto)
        {
            var createdPermissionId = await _permissionAppService.CreatePermissionAsync(permissionDto);
            var response = new ApiResponse<int>(true, "Permission created successfully", createdPermissionId, 201);
            return CreatedAtAction(nameof(GetPermissionById), new { id = createdPermissionId }, response);
        }

        /// <summary>
        /// Updates an existing permission.
        /// </summary>
        /// <param name="id">The ID of the permission to update.</param>
        /// <param name="permissionDto">The updated details of the permission.</param>
        /// <returns>No content if successful.</returns>
        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResponse<string>>> UpdatePermission(int id, [FromBody] PermissionDto permissionDto)
        {
            await _permissionAppService.UpdatePermissionAsync(id, permissionDto);
            var response = new ApiResponse<string>(true, "Permission updated successfully", null, 204);
            return Ok(response);
        }

        /// <summary>
        /// Deletes a permission by ID.
        /// </summary>
        /// <param name="id">The ID of the permission to delete.</param>
        /// <returns>No content if successful.</returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse<string>>> DeletePermission(int id)
        {
            await _permissionAppService.DeletePermissionAsync(id);
            var response = new ApiResponse<string>(true, "Permission deleted successfully", null, 204);
            return Ok(response);
        }
    }
}
