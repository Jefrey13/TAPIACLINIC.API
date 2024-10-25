using Application.Commands.Schedules;
using Application.Models.ReponseDtos;
using Application.Models.RequestDtos;
using Application.Services;
using API.Utils;
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
        public async Task<ActionResult<ApiResponse<IEnumerable<ScheduleResponseDto>>>> GetAllSchedules()
        {
            var schedules = await _scheduleAppService.GetAllSchedulesAsync();

            if (schedules == null || !schedules.Any())
            {
                return NotFound(new ApiResponse<IEnumerable<ScheduleResponseDto>>(false, "No schedules found", null, 404));
            }

            var response = new ApiResponse<IEnumerable<ScheduleResponseDto>>(true, "Schedules retrieved successfully", schedules, 200);
            return Ok(response);
        }

        /// <summary>
        /// Retrieves a specific schedule by its ID.
        /// </summary>
        /// <param name="id">The ID of the schedule to retrieve.</param>
        /// <returns>The ScheduleResponseDto of the requested schedule, or 404 if not found.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<ScheduleResponseDto>>> GetScheduleById(int id)
        {
            if (id <= 0)
            {
                return BadRequest(new ApiResponse<ScheduleResponseDto>(false, "Invalid schedule ID", null, 400));
            }

            var schedule = await _scheduleAppService.GetScheduleByIdAsync(id);
            if (schedule == null)
            {
                return NotFound(new ApiResponse<ScheduleResponseDto>(false, "Schedule not found", null, 404));
            }

            var response = new ApiResponse<ScheduleResponseDto>(true, "Schedule retrieved successfully", schedule, 200);
            return Ok(response);
        }

        /// <summary>
        /// Creates a new schedule.
        /// </summary>
        /// <param name="scheduleDto">The details of the schedule to be created.</param>
        /// <returns>The ID of the newly created schedule.</returns>
        [HttpPost]
        public async Task<ActionResult<ApiResponse<int>>> CreateSchedule([FromBody] ScheduleResquestDto scheduleDto)
        {
            if (scheduleDto == null)
            {
                return BadRequest(new ApiResponse<int?>(false, "Schedule data is required", null, 400));
            }

            var createdScheduleId = await _scheduleAppService.CreateScheduleAsync(new CreateScheduleCommand(scheduleDto));
            var response = new ApiResponse<int>(true, "Schedule created successfully", createdScheduleId, 201);
            return CreatedAtAction(nameof(GetScheduleById), new { id = createdScheduleId }, response);
        }

        /// <summary>
        /// Updates an existing schedule.
        /// </summary>
        /// <param name="id">The ID of the schedule to update.</param>
        /// <param name="scheduleDto">The updated details of the schedule.</param>
        /// <returns>No content if the update is successful, 400 if the ID mismatch occurs.</returns>
        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResponse<string>>> UpdateSchedule(int id, [FromBody] ScheduleResquestDto scheduleDto)
        {
            if (id <= 0 || scheduleDto == null)
            {
                return BadRequest(new ApiResponse<string>(false, "Invalid schedule ID or data", null, 400));
            }

            var existingSchedule = await _scheduleAppService.GetScheduleByIdAsync(id);
            if (existingSchedule == null)
            {
                return NotFound(new ApiResponse<string>(false, "Schedule not found", null, 404));
            }

            await _scheduleAppService.UpdateScheduleAsync(new UpdateScheduleCommand(id, scheduleDto));
            var response = new ApiResponse<string>(true, "Schedule updated successfully", null, 200);
            return Ok(response);
        }

        /// <summary>
        /// Deletes a schedule by its ID.
        /// </summary>
        /// <param name="id">The ID of the schedule to delete.</param>
        /// <returns>No content if the deletion is successful.</returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse<string>>> DeleteSchedule(int id)
        {
            if (id <= 0)
            {
                return BadRequest(new ApiResponse<string>(false, "Invalid schedule ID", null, 400));
            }

            var existingSchedule = await _scheduleAppService.GetScheduleByIdAsync(id);
            if (existingSchedule == null)
            {
                return NotFound(new ApiResponse<string>(false, "Schedule not found", null, 404));
            }

            await _scheduleAppService.DeleteScheduleAsync(id);
            var response = new ApiResponse<string>(true, "Schedule deleted successfully", null, 204);
            return Ok(response);
        }

        /// <summary>
        /// Retrieves schedules associated with a specific specialty.
        /// </summary>
        /// <param name="specialtyId">The ID of the specialty to filter schedules.</param>
        /// <returns>A list of schedules associated with the specialty.</returns>
        [HttpGet("by-specialty/{specialtyId}")]
        public async Task<ActionResult<ApiResponse<IEnumerable<ScheduleResponseDto>>>> GetSchedulesBySpecialty(int specialtyId)
        {
            if (specialtyId <= 0)
            {
                return BadRequest(new ApiResponse<IEnumerable<ScheduleResponseDto>>(false, "Invalid specialty ID", null, 400));
            }

            var schedules = await _scheduleAppService.GetSchedulesBySpecialtyAsync(specialtyId);
            if (schedules == null || !schedules.Any())
            {
                return NotFound(new ApiResponse<IEnumerable<ScheduleResponseDto>>(false, "No schedules found for this specialty", null, 404));
            }

            var response = new ApiResponse<IEnumerable<ScheduleResponseDto>>(true, "Schedules by specialty retrieved successfully", schedules, 200);
            return Ok(response);
        }
    }
}