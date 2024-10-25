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

            if (permissions == null || !permissions.Any())
            {
                return NotFound(new ApiResponse<IEnumerable<PermissionDto>>(false, "No permissions found", null, 404));
            }

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
            if (id <= 0)
            {
                return BadRequest(new ApiResponse<PermissionDto>(false, "Invalid permission ID", null, 400));
            }

            var permission = await _permissionAppService.GetPermissionByIdAsync(id);
            if (permission == null)
            {
                return NotFound(new ApiResponse<PermissionDto>(false, "Permission not found", null, 404));
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
            if (permissionDto == null)
            {
                return BadRequest(new ApiResponse<int?>(false, "Permission data is required", null, 400));
            }

            var createdPermissionId = await _permissionAppService.CreatePermissionAsync(permissionDto);
            if (createdPermissionId <= 0)
            {
                return StatusCode(500, new ApiResponse<int?>(false, "Failed to create permission", null, 500));
            }

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
            if (id <= 0 || permissionDto == null)
            {
                return BadRequest(new ApiResponse<string>(false, "Invalid permission ID or data", null, 400));
            }

            var permissionExists = await _permissionAppService.GetPermissionByIdAsync(id);
            if (permissionExists == null)
            {
                return NotFound(new ApiResponse<string>(false, "Permission not found", null, 404));
            }

            await _permissionAppService.UpdatePermissionAsync(id, permissionDto);
            var response = new ApiResponse<string>(true, "Permission updated successfully", null, 200);
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
            if (id <= 0)
            {
                return BadRequest(new ApiResponse<string>(false, "Invalid permission ID", null, 400));
            }

            var permissionExists = await _permissionAppService.GetPermissionByIdAsync(id);
            if (permissionExists == null)
            {
                return NotFound(new ApiResponse<string>(false, "Permission not found", null, 404));
            }

            await _permissionAppService.DeletePermissionAsync(id);
            var response = new ApiResponse<string>(true, "Permission deleted successfully", null, 204);
            return Ok(response);
        }
    }
}