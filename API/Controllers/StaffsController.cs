using Application.Commands.Staffs;
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
    /// API controller for managing staff members.
    /// Provides endpoints for creating, updating, deleting, and retrieving staff members.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class StaffsController : ControllerBase
    {
        private readonly IStaffAppService _staffAppService;

        /// <summary>
        /// Constructor to initialize the StaffAppService dependency.
        /// </summary>
        /// <param name="staffAppService">Service for handling staff operations.</param>
        public StaffsController(IStaffAppService staffAppService)
        {
            _staffAppService = staffAppService;
        }

        /// <summary>
        /// Retrieves all staff members in the system.
        /// </summary>
        /// <returns>A list of StaffResponseDto objects representing all staff members.</returns>
        [HttpGet]
        public async Task<ActionResult<ApiResponse<IEnumerable<StaffResponseDto>>>> GetAllStaffs()
        {
            var staffs = await _staffAppService.GetAllStaffsAsync();
            var response = new ApiResponse<IEnumerable<StaffResponseDto>>(true, "Staffs retrieved successfully", staffs, 200);
            return Ok(response);
        }

        /// <summary>
        /// Retrieves a specific staff member by their ID.
        /// </summary>
        /// <param name="id">The ID of the staff member to retrieve.</param>
        /// <returns>The StaffResponseDto of the requested staff member, or 404 if not found.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<StaffResponseDto>>> GetStaffById(int id)
        {
            var staff = await _staffAppService.GetStaffByIdAsync(id);
            if (staff == null)
            {
                var errorResponse = new ApiResponse<StaffResponseDto>(false, "Staff not found", null, 404);
                return NotFound(errorResponse);
            }
            var response = new ApiResponse<StaffResponseDto>(true, "Staff retrieved successfully", staff, 200);
            return Ok(response);
        }

        /// <summary>
        /// Retrieves a specific staff member by their associated User ID.
        /// </summary>
        /// <param name="userId">The User ID of the staff member to retrieve.</param>
        /// <returns>The StaffResponseDto of the requested staff member, or 404 if not found.</returns>
        [HttpGet("by-user/{userId}")]
        public async Task<ActionResult<ApiResponse<StaffResponseDto>>> GetStaffByUserId(int userId)
        {
            var staff = await _staffAppService.GetStaffByUserIdAsync(userId);
            if (staff == null)
            {
                var errorResponse = new ApiResponse<StaffResponseDto>(false, "Staff not found", null, 404);
                return NotFound(errorResponse);
            }
            var response = new ApiResponse<StaffResponseDto>(true, "Staff retrieved successfully", staff, 200);
            return Ok(response);
        }

        /// <summary>
        /// Retrieves all staff members by their associated Specialty ID.
        /// </summary>
        /// <param name="specialtyId">The Specialty ID of the staff members to retrieve.</param>
        /// <returns>A list of StaffResponseDto objects representing staff members in the specified specialty.</returns>
        [HttpGet("by-specialty/{specialtyId}")]
        public async Task<ActionResult<ApiResponse<IEnumerable<StaffResponseDto>>>> GetStaffBySpecialtyId(int specialtyId)
        {
            var staffs = await _staffAppService.GetStaffBySpecialtyIdAsync(specialtyId);
            var response = new ApiResponse<IEnumerable<StaffResponseDto>>(true, "Staffs by specialty retrieved successfully", staffs, 200);
            return Ok(response);
        }

        /// <summary>
        /// Creates a new staff member.
        /// </summary>
        /// <param name="staffDto">The staff member data to create.</param>
        /// <returns>The ID of the newly created staff member.</returns>
        [HttpPost]
        public async Task<ActionResult<ApiResponse<int>>> CreateStaff([FromBody] StaffRequestDto staffDto)
        {
            var createdStaffId = await _staffAppService.CreateStaffAsync(new CreateStaffCommand(staffDto));
            var response = new ApiResponse<int>(true, "Staff created successfully", createdStaffId, 201);
            return CreatedAtAction(nameof(GetStaffById), new { id = createdStaffId }, response);
        }

        /// <summary>
        /// Updates an existing staff member.
        /// </summary>
        /// <param name="id">The ID of the staff member to update.</param>
        /// <param name="staffDto">The updated staff member data.</param>
        /// <returns>No content if the update is successful, 400 if the ID mismatch occurs.</returns>
        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResponse<string>>> UpdateStaff(int id, [FromBody] StaffRequestDto staffDto)
        {
            if (id <= 0)
            {
                var errorResponse = new ApiResponse<string>(false, "Invalid staff ID", null, 400);
                return BadRequest(errorResponse);
            }

            await _staffAppService.UpdateStaffAsync(new UpdateStaffCommand(id, staffDto));
            var response = new ApiResponse<string>(true, "Staff updated successfully", null, 204);
            return Ok(response);
        }

        /// <summary>
        /// Deletes a staff member by their ID.
        /// </summary>
        /// <param name="id">The ID of the staff member to delete.</param>
        /// <returns>No content if the deletion is successful.</returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse<string>>> DeleteStaff(int id)
        {
            if (id <= 0)
            {
                var errorResponse = new ApiResponse<string>(false, "Invalid staff ID", null, 400);
                return BadRequest(errorResponse);
            }

            await _staffAppService.DeleteStaffAsync(new DeleteStaffCommand(id));
            var response = new ApiResponse<string>(true, "Staff deleted successfully", null, 204);
            return Ok(response);
        }
    }
}