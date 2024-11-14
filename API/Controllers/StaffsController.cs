using Application.Commands.Staffs;
using Application.Models.ReponseDtos;
using Application.Models.RequestDtos;
using Application.Services;
using API.Utils;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

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

            if (staffs == null || !staffs.Any())
            {
                return NotFound(new ApiResponse<IEnumerable<StaffResponseDto>>(false, "No staff members found", null, 404));
            }

            var response = new ApiResponse<IEnumerable<StaffResponseDto>>(true, "Staff members retrieved successfully", staffs, 200);
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
            if (id <= 0)
            {
                return BadRequest(new ApiResponse<StaffResponseDto>(false, "Invalid staff ID", null, 400));
            }

            var staff = await _staffAppService.GetStaffByIdAsync(id);
            if (staff == null)
            {
                return NotFound(new ApiResponse<StaffResponseDto>(false, "Staff member not found", null, 404));
            }

            var response = new ApiResponse<StaffResponseDto>(true, "Staff member retrieved successfully", staff, 200);
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
            if (userId <= 0)
            {
                return BadRequest(new ApiResponse<StaffResponseDto>(false, "Invalid user ID", null, 400));
            }

            var staff = await _staffAppService.GetStaffByUserIdAsync(userId);
            if (staff == null)
            {
                return NotFound(new ApiResponse<StaffResponseDto>(false, "Staff member not found for this user", null, 404));
            }

            var response = new ApiResponse<StaffResponseDto>(true, "Staff member retrieved successfully", staff, 200);
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
            if (specialtyId <= 0)
            {
                return BadRequest(new ApiResponse<IEnumerable<StaffResponseDto>>(false, "Invalid specialty ID", null, 400));
            }

            var staffs = await _staffAppService.GetStaffBySpecialtyIdAsync(specialtyId);

            if (staffs == null || !staffs.Any())
            {
                return NotFound(new ApiResponse<IEnumerable<StaffResponseDto>>(false, "No staff members found for this specialty", null, 404));
            }

            var response = new ApiResponse<IEnumerable<StaffResponseDto>>(true, "Staff members retrieved by specialty successfully", staffs, 200);
            return Ok(response);
        }

        /// <summary>
        /// Creates a new staff member.
        /// </summary>
        /// <param name="staffDto">The staff member data to create.</param>
        /// <returns>The ID of the newly created staff member.</returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<ApiResponse<int>>> CreateStaff([FromBody] StaffRequestDto staffDto)
        {
            if (staffDto == null)
            {
                return BadRequest(new ApiResponse<int?>(false, "Staff data is required", null, 400));
            }

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
            if (id <= 0 || staffDto == null)
            {
                return BadRequest(new ApiResponse<string>(false, "Invalid staff ID or data", null, 400));
            }

            var existingStaff = await _staffAppService.GetStaffByIdAsync(id);
            if (existingStaff == null)
            {
                return NotFound(new ApiResponse<string>(false, "Staff member not found", null, 404));
            }

            await _staffAppService.UpdateStaffAsync(new UpdateStaffCommand(id, staffDto));
            var response = new ApiResponse<string>(true, "Staff updated successfully", null, 200);
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
                return BadRequest(new ApiResponse<string>(false, "Invalid staff ID", null, 400));
            }

            var existingStaff = await _staffAppService.GetStaffByIdAsync(id);
            if (existingStaff == null)
            {
                return NotFound(new ApiResponse<string>(false, "Staff member not found", null, 404));
            }

            await _staffAppService.DeleteStaffAsync(new DeleteStaffCommand(id));
            var response = new ApiResponse<string>(true, "Staff deleted successfully", null, 204);
            return Ok(response);
        }
        /// <summary>
        /// Retrieves all staff members with the specified state ID.
        /// </summary>
        /// <param name="stateId">The State ID of the staff members to retrieve.</param>
        /// <returns>A list of StaffResponseDto objects representing staff members in the specified state.</returns>
        [HttpGet("by-state/{stateId}")]
        public async Task<ActionResult<ApiResponse<IEnumerable<StaffResponseDto>>>> GetStaffByState(int stateId)
        {
            if (stateId <= 0)
            {
                return BadRequest(new ApiResponse<IEnumerable<StaffResponseDto>>(false, "Invalid state ID", null, 400));
            }

            var staffs = await _staffAppService.GetStaffByStateAsync(stateId);

            if (staffs == null || !staffs.Any())
            {
                return NotFound(new ApiResponse<IEnumerable<StaffResponseDto>>(false, "No staff members found for the specified state", null, 404));
            }

            var response = new ApiResponse<IEnumerable<StaffResponseDto>>(true, "Staff members retrieved by state successfully", staffs, 200);
            return Ok(response);
        }
    }
}