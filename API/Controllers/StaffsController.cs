using Application.Commands.Staffs;
using Application.Models.ReponseDtos;
using Application.Models.RequestDtos;
using Application.Services;
using API.Utils;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;
using Application.Models.RequestDtos.UpdateRequestDto;

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
            try
            {
                var staffs = await _staffAppService.GetAllStaffsAsync();

                if (staffs == null || !staffs.Any())
                {
                    return ResponseHelper.NotFound<IEnumerable<StaffResponseDto>>("No staff members found.");
                }

                return ResponseHelper.Success(staffs, "Staff members retrieved successfully.");
            }
            catch (Exception ex)
            {
                return ResponseHelper.Error<IEnumerable<StaffResponseDto>>($"An error occurred: {ex.Message}");
            }
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
                return ResponseHelper.BadRequest<StaffResponseDto>("Invalid staff ID.");
            }

            try
            {
                var staff = await _staffAppService.GetStaffByIdAsync(id);
                if (staff == null)
                {
                    return ResponseHelper.NotFound<StaffResponseDto>("Staff member not found.");
                }

                return ResponseHelper.Success(staff, "Staff member retrieved successfully.");
            }
            catch (Exception ex)
            {
                return ResponseHelper.Error<StaffResponseDto>($"An error occurred: {ex.Message}");
            }
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
                return ResponseHelper.BadRequest<StaffResponseDto>("Invalid user ID.");
            }

            try
            {
                var staff = await _staffAppService.GetStaffByUserIdAsync(userId);
                if (staff == null)
                {
                    return ResponseHelper.NotFound<StaffResponseDto>("Staff member not found for this user.");
                }

                return ResponseHelper.Success(staff, "Staff member retrieved successfully.");
            }
            catch (Exception ex)
            {
                return ResponseHelper.Error<StaffResponseDto>($"An error occurred: {ex.Message}");
            }
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
                return ResponseHelper.BadRequest<IEnumerable<StaffResponseDto>>("Invalid specialty ID.");
            }

            try
            {
                var staffs = await _staffAppService.GetStaffBySpecialtyIdAsync(specialtyId);

                if (staffs == null || !staffs.Any())
                {
                    return ResponseHelper.NotFound<IEnumerable<StaffResponseDto>>("No staff members found for this specialty.");
                }

                return ResponseHelper.Success(staffs, "Staff members retrieved by specialty successfully.");
            }
            catch (Exception ex)
            {
                return ResponseHelper.Error<IEnumerable<StaffResponseDto>>($"An error occurred: {ex.Message}");
            }
        }

        /// <summary>
        /// Creates a new staff member.
        /// </summary>
        /// <param name="staffDto">The staff member data to create.</param>
        /// <returns>The ID of the newly created staff member.</returns>
        [HttpPost]
        public async Task<ActionResult<ApiResponse<StaffResponseDto>>> CreateStaff([FromBody] StaffRequestDto staffDto)
        {
            if (staffDto == null)
            {
                return ResponseHelper.BadRequest<StaffResponseDto>("Staff data is required.");
            }

            try
            {
                var createdStaffId = await _staffAppService.CreateStaffAsync(new CreateStaffCommand(staffDto));
                if (createdStaffId == false)
                {
                    return ResponseHelper.Error<StaffResponseDto>("Failed to create staff member.");
                }

                return ResponseHelper.Success(new StaffResponseDto(), "Staff created successfully.");
            }
            catch (Exception ex)
            {
                return ResponseHelper.Error<StaffResponseDto>($"An error occurred: {ex.Message}");
            }
        }
        /// <summary>
        /// Updates an existing staff member.
        /// </summary>
        /// <param name="id">The ID of the staff member to update.</param>
        /// <param name="staffDto">The updated staff member data.</param>
        /// <returns>An ApiResponse indicating the result of the operation.</returns>
        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResponse<StaffResponseDto>>> UpdateStaff(int id, [FromBody] StaffUpdateRequestDto staffDto)
        {
            if (id <= 0 || staffDto == null)
            {
                return ResponseHelper.BadRequest<StaffResponseDto>("Invalid staff ID or data.");
            }

            try
            {
                var existingStaff = await _staffAppService.GetStaffByIdAsync(id);
                if (existingStaff == null)
                {
                    return ResponseHelper.NotFound<StaffResponseDto>("Staff member not found.");
                }

                var isUpdated = await _staffAppService.UpdateStaffAsync(new UpdateStaffCommand(id, staffDto));
                if (!isUpdated)
                {
                    return ResponseHelper.Error<StaffResponseDto>("Failed to update the staff member.");
                }

                return ResponseHelper.Success<StaffResponseDto>(new StaffResponseDto(), "Staff updated successfully.");
            }
            catch (Exception ex)
            {
                return ResponseHelper.Error<StaffResponseDto>($"An error occurred: {ex.Message}");
            }
        }

        /// <summary>
        /// Updates the state of a staff member by their ID.
        /// </summary>
        /// <param name="id">The ID of the staff member whose state is to be updated.</param>
        /// <returns>An ApiResponse indicating if the state update was successful.</returns>
        [HttpPatch("{id}/change-state")]
        public async Task<ActionResult<ApiResponse<StaffResponseDto>>> UpdateStaffState(int id)
        {
            if (id <= 0)
            {
                return ResponseHelper.BadRequest<StaffResponseDto>("Invalid staff ID.");
            }

            try
            {
                var staffMember = await _staffAppService.GetStaffByIdAsync(id);
                if (staffMember == null)
                {
                    return ResponseHelper.NotFound<StaffResponseDto>("Staff member not found.");
                }

                var isUpdated = await _staffAppService.DeleteStaffAsync(new DeleteStaffCommand(id));
                if (!isUpdated)
                {
                    return ResponseHelper.Error<StaffResponseDto>("Failed to update the staff member's state.");
                }

                return ResponseHelper.Success<StaffResponseDto>(new StaffResponseDto(), "Staff state updated successfully.");
            }
            catch (Exception ex)
            {
                return ResponseHelper.Error<StaffResponseDto>($"An error occurred: {ex.Message}");
            }
        }

        /// <summary>
        /// Retrieves all staff members associated with the specified state ID.
        /// </summary>
        /// <param name="stateId">The unique identifier of the state for which staff members are to be retrieved.</param>
        /// <returns>
        /// An HTTP response containing:
        /// - 200 (OK) with a list of StaffResponseDto objects if staff members are found.
        /// - 400 (Bad Request) if the provided state ID is invalid.
        /// - 404 (Not Found) if no staff members are found for the specified state.
        /// - 500 (Internal Server Error) if an unexpected error occurs.
        /// </returns>
        /// <remarks>
        /// This endpoint performs input validation, fetches data using the staff application service,
        /// and provides a standardized response for success or failure scenarios.
        /// </remarks>
        [HttpGet("by-state/{stateId}")]
        public async Task<ActionResult<ApiResponse<IEnumerable<StaffResponseDto>>>> GetStaffByState(int stateId)
        {
            // Validate the state ID input
            if (stateId <= 0)
            {
                // Return a 400 Bad Request response if the state ID is invalid
                return ResponseHelper.BadRequest<IEnumerable<StaffResponseDto>>(
                    "Invalid state ID. The ID must be greater than zero.");
            }

            try
            {
                // Query the application service to fetch staff members by state ID
                var staffs = await _staffAppService.GetStaffByStateAsync(stateId);

                // Check if any staff members are found for the given state ID
                if (staffs == null || !staffs.Any())
                {
                    // Return a 404 Not Found response if no staff members are found
                    return ResponseHelper.NotFound<IEnumerable<StaffResponseDto>>(
                        "No staff members found for the specified state.");
                }

                // Return a 200 OK response with the retrieved staff members
                return ResponseHelper.Success(
                    staffs,
                    "Staff members retrieved successfully by state.");
            }
            catch (Exception ex)
            {
                // Log the exception for debugging and monitoring purposes (not shown here for simplicity)

                // Return a 500 Internal Server Error response with the exception message
                return ResponseHelper.Error<IEnumerable<StaffResponseDto>>(
                    $"An unexpected error occurred while retrieving staff members: {ex.Message}");
            }
        }


        /// <summary>
        /// Retrieves all staff members with the specified role.
        /// </summary>
        /// <param name="roleName">The role name of the staff members to retrieve.</param>
        /// <returns>A list of StaffResponseDto objects representing staff members in the specified role.</returns>
        [HttpGet("by-role/{roleName}")]
        public async Task<ActionResult<ApiResponse<IEnumerable<StaffResponseDto>>>> GetStaffByRole(string roleName)
        {
            if (string.IsNullOrEmpty(roleName))
            {
                return ResponseHelper.BadRequest<IEnumerable<StaffResponseDto>>("Invalid role name.");
            }

            try
            {
                var staffs = await _staffAppService.GetStaffByRoleAsync(roleName);
                if (staffs == null || !staffs.Any())
                {
                    return ResponseHelper.NotFound<IEnumerable<StaffResponseDto>>("No staff members found for the specified role.");
                }

                return ResponseHelper.Success(staffs, "Staff members retrieved successfully by role.");
            }
            catch (Exception ex)
            {
                return ResponseHelper.Error<IEnumerable<StaffResponseDto>>($"An error occurred: {ex.Message}");
            }
        }
    }
}