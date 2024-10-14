using Application.Commands.Staffs;
using Application.Models;
using Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StaffController : ControllerBase
    {
        private readonly IStaffAppService _staffAppService;

        public StaffController(IStaffAppService staffAppService)
        {
            _staffAppService = staffAppService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<StaffDto>>> GetAllStaffs()
        {
            var staffs = await _staffAppService.GetAllStaffsAsync();
            return Ok(staffs);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<StaffDto>> GetStaffById(int id)
        {
            var staff = await _staffAppService.GetStaffByIdAsync(id);
            if (staff == null)
            {
                return NotFound();
            }
            return Ok(staff);
        }

        [HttpPost]
        public async Task<ActionResult<int>> CreateStaff([FromBody] CreateStaffCommand command)
        {
            var createdStaffId = await _staffAppService.CreateStaffAsync(command);
            return CreatedAtAction(nameof(GetStaffById), new { id = createdStaffId }, createdStaffId);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStaff(int id, [FromBody] UpdateStaffCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest("Staff ID in the request does not match the one in the body.");
            }

            await _staffAppService.UpdateStaffAsync(command);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStaff(int id)
        {
            await _staffAppService.DeleteStaffAsync(id);
            return NoContent();
        }
    }
}