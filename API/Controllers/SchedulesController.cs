using Application.Commands;
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
            var schedule = await _scheduleAppService.GetScheduleByIdAsync(id);
            if (schedule == null)
            {
                var errorResponse = new ApiResponse<ScheduleResponseDto>(false, "Schedule not found", null, 404);
                return NotFound(errorResponse);
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
            // Validación para verificar que el ID no sea cero o negativo
            if (id <= 0)
            {
                var errorResponse = new ApiResponse<string>(false, "Invalid ID", null, 400);
                return BadRequest(errorResponse);
            }

            // Si el ID es válido, proceder con la actualización
            await _scheduleAppService.UpdateScheduleAsync(new UpdateScheduleCommand(id, scheduleDto));
            var response = new ApiResponse<string>(true, "Schedule updated successfully", null, 204);
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
            var schedules = await _scheduleAppService.GetSchedulesBySpecialtyAsync(specialtyId);
            var response = new ApiResponse<IEnumerable<ScheduleResponseDto>>(true, "Schedules by specialty retrieved successfully", schedules, 200);
            return Ok(response);
        }
    }
}