using Application.Commands;
using Application.Commands.Schedules;
using Application.Models.ReponseDtos;
using Application.Models.RequestDtos;
using Application.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SchedulesController : ControllerBase
    {
        private readonly IScheduleAppService _scheduleAppService;

        public SchedulesController(IScheduleAppService scheduleAppService)
        {
            _scheduleAppService = scheduleAppService;
        }

        /// <summary>
        /// Retrieves all schedules available in the system.
        /// </summary>
        /// <returns>A list of ScheduleResponseDto with details of all schedules.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ScheduleResponseDto>>> GetAllSchedules()
        {
            var schedules = await _scheduleAppService.GetAllSchedulesAsync();
            return Ok(schedules);
        }

        /// <summary>
        /// Retrieves a specific schedule by its ID.
        /// </summary>
        /// <param name="id">The ID of the schedule to retrieve.</param>
        /// <returns>The ScheduleResponseDto of the requested schedule, or 404 if not found.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<ScheduleResponseDto>> GetScheduleById(int id)
        {
            var schedule = await _scheduleAppService.GetScheduleByIdAsync(id);
            if (schedule == null)
            {
                return NotFound();
            }
            return Ok(schedule);
        }

        /// <summary>
        /// Creates a new schedule.
        /// </summary>
        /// <param name="scheduleDto">The details of the schedule to be created.</param>
        /// <returns>The ID of the newly created schedule.</returns>
        [HttpPost]
        public async Task<ActionResult<int>> CreateSchedule([FromBody] ScheduleResquestDto scheduleDto)
        {
            var createdScheduleId = await _scheduleAppService.CreateScheduleAsync(new CreateScheduleCommand(scheduleDto));
            return CreatedAtAction(nameof(GetScheduleById), new { id = createdScheduleId }, createdScheduleId);
        }

        /// <summary>
        /// Updates an existing schedule.
        /// </summary>
        /// <param name="id">The ID of the schedule to update.</param>
        /// <param name="scheduleDto">The updated details of the schedule.</param>
        /// <returns>No content if the update is successful, 400 if the ID mismatch occurs.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSchedule(int id, [FromBody] ScheduleResquestDto scheduleDto)
        {
            if (id != id)
            {
                return BadRequest("Schedule ID in the request does not match the one in the body.");
            }

            await _scheduleAppService.UpdateScheduleAsync(new UpdateScheduleCommand(id, scheduleDto));
            return NoContent();
        }

        /// <summary>
        /// Deletes a schedule by its ID.
        /// </summary>
        /// <param name="id">The ID of the schedule to delete.</param>
        /// <returns>No content if the deletion is successful.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSchedule(int id)
        {
            await _scheduleAppService.DeleteScheduleAsync(id);
            return NoContent();
        }

        /// <summary>
        /// Retrieves schedules associated with a specific specialty.
        /// </summary>
        /// <param name="specialtyId">The ID of the specialty to filter schedules.</param>
        /// <returns>A list of schedules associated with the specialty.</returns>
        [HttpGet("by-specialty/{specialtyId}")]
        public async Task<ActionResult<IEnumerable<ScheduleResponseDto>>> GetSchedulesBySpecialty(int specialtyId)
        {
            var schedules = await _scheduleAppService.GetSchedulesBySpecialtyAsync(specialtyId);
            return Ok(schedules);
        }
    }
}