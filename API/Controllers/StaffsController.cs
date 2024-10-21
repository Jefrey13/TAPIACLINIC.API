using Application.Commands.Staffs;
using Application.Models.ReponseDtos;
using Application.Models.RequestDtos;
using Application.Services;
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
        public async Task<ActionResult<IEnumerable<StaffResponseDto>>> GetAllStaffs()
        {
            var staffs = await _staffAppService.GetAllStaffsAsync();
            return Ok(staffs);
        }

        /// <summary>
        /// Retrieves a specific staff member by their ID.
        /// </summary>
        /// <param name="id">The ID of the staff member to retrieve.</param>
        /// <returns>The StaffResponseDto of the requested staff member, or 404 if not found.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<StaffResponseDto>> GetStaffById(int id)
        {
            var staff = await _staffAppService.GetStaffByIdAsync(id);
            if (staff == null)
            {
                return NotFound();
            }
            return Ok(staff);
        }

        /// <summary>
        /// Retrieves a specific staff member by their associated User ID.
        /// </summary>
        /// <param name="userId">The User ID of the staff member to retrieve.</param>
        /// <returns>The StaffResponseDto of the requested staff member, or 404 if not found.</returns>
        [HttpGet("by-user/{userId}")]
        public async Task<ActionResult<StaffResponseDto>> GetStaffByUserId(int userId)
        {
            var staff = await _staffAppService.GetStaffByUserIdAsync(userId);
            if (staff == null)
            {
                return NotFound();
            }
            return Ok(staff);
        }

        /// <summary>
        /// Retrieves all staff members by their associated Specialty ID.
        /// </summary>
        /// <param name="specialtyId">The Specialty ID of the staff members to retrieve.</param>
        /// <returns>A list of StaffResponseDto objects representing staff members in the specified specialty.</returns>
        [HttpGet("by-specialty/{specialtyId}")]
        public async Task<ActionResult<IEnumerable<StaffResponseDto>>> GetStaffBySpecialtyId(int specialtyId)
        {
            var staffs = await _staffAppService.GetStaffBySpecialtyIdAsync(specialtyId);
            return Ok(staffs);
        }

        /// <summary>
        /// Creates a new staff member.
        /// </summary>
        /// <param name="staffDto">The staff member data to create.</param>
        /// <returns>The ID of the newly created staff member.</returns>
        [HttpPost]
        public async Task<ActionResult<int>> CreateStaff([FromBody] StaffRequestDto staffDto)
        {
            var createdStaffId = await _staffAppService.CreateStaffAsync(new CreateStaffCommand(staffDto));
            return CreatedAtAction(nameof(GetStaffById), new { id = createdStaffId }, createdStaffId);
        }

        /// <summary>
        /// Updates an existing staff member.
        /// </summary>
        /// <param name="id">The ID of the staff member to update.</param>
        /// <param name="staffDto">The updated staff member data.</param>
        /// <returns>No content if the update is successful, 400 if the ID mismatch occurs.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStaff(int id, [FromBody] StaffRequestDto staffDto)
        {
            if (id <= 0)
            {
                return BadRequest("Invalid staff ID.");
            }

            await _staffAppService.UpdateStaffAsync(new UpdateStaffCommand(id, staffDto));
            return NoContent();
        }

        /// <summary>
        /// Deletes a staff member by their ID.
        /// </summary>
        /// <param name="id">The ID of the staff member to delete.</param>
        /// <returns>No content if the deletion is successful.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStaff(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Invalid staff ID.");
            }

            await _staffAppService.DeleteStaffAsync(new DeleteStaffCommand(id));
            return NoContent();
        }
    }
}