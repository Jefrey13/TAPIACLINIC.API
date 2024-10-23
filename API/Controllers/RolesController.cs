using Application.Commands.Roles;
using Application.Models.ReponseDtos;
using Application.Models.RequestDtos;
using Application.Services;
using API.Utils;
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
        /// <returns>A list of RoleResponseDto objects representing all roles.</returns>
        [HttpGet]
        public async Task<ActionResult<ApiResponse<IEnumerable<RoleResponseDto>>>> GetAllRoles()
        {
            var roles = await _roleAppService.GetAllRolesAsync();
            var response = new ApiResponse<IEnumerable<RoleResponseDto>>(true, "Roles retrieved successfully", roles, 200);
            return Ok(response);
        }

        /// <summary>
        /// Retrieves a specific role by its ID.
        /// </summary>
        /// <param name="id">The ID of the role to retrieve.</param>
        /// <returns>The RoleResponseDto of the requested role, or 404 if not found.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<RoleResponseDto>>> GetRoleById(int id)
        {
            var role = await _roleAppService.GetRoleByIdAsync(id);
            if (role == null)
            {
                var errorResponse = new ApiResponse<RoleResponseDto>(false, "Role not found", null, 404);
                return NotFound(errorResponse);
            }

            var response = new ApiResponse<RoleResponseDto>(true, "Role retrieved successfully", role, 200);
            return Ok(response);
        }

        /// <summary>
        /// Creates a new role.
        /// </summary>
        /// <param name="command">The command containing role details.</param>
        /// <returns>The ID of the newly created role.</returns>
        [HttpPost]
        public async Task<ActionResult<ApiResponse<int>>> CreateRole([FromBody] CreateRoleCommand command)
        {
            var createdRoleId = await _roleAppService.CreateRoleAsync(command);
            var response = new ApiResponse<int>(true, "Role created successfully", createdRoleId, 201);
            return CreatedAtAction(nameof(GetRoleById), new { id = createdRoleId }, response);
        }

        /// <summary>
        /// Updates an existing role.
        /// </summary>
        /// <param name="id">The ID of the role to update.</param>
        /// <param name="roleDto">The updated role details.</param>
        /// <returns>No content if the update is successful, 400 if the ID mismatch occurs.</returns>
        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResponse<string>>> UpdateRole(int id, [FromBody] RoleRequestDto roleDto)
        {
            var command = new UpdateRoleCommand(id, roleDto);
            await _roleAppService.UpdateRoleAsync(id, command);
            var response = new ApiResponse<string>(true, "Role updated successfully", null, 204);
            return Ok(response);
        }

        /// <summary>
        /// Deletes a role by its ID.
        /// </summary>
        /// <param name="id">The ID of the role to delete.</param>
        /// <returns>No content if the deletion is successful.</returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse<string>>> DeleteRole(int id)
        {
            await _roleAppService.DeleteRoleAsync(new DeleteRoleCommand(id));
            var response = new ApiResponse<string>(true, "Role deleted successfully", null, 204);
            return Ok(response);
        }
    }
}