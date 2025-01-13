using Application.Commands.Schedules;
using Application.Models.ReponseDtos;
using Application.Models.RequestDtos;
using Application.Services;
using API.Utils;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
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
            try
            {
                var schedules = await _scheduleAppService.GetAllSchedulesAsync();
                if (schedules == null || !schedules.Any())
                {
                    return ResponseHelper.NotFound<IEnumerable<ScheduleResponseDto>>("No schedules found.");
                }

                return ResponseHelper.Success(schedules, "Schedules retrieved successfully.");
            }
            catch (Exception ex)
            {
                return ResponseHelper.Error<IEnumerable<ScheduleResponseDto>>($"An error occurred: {ex.Message}");
            }
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
                return ResponseHelper.BadRequest<ScheduleResponseDto>("Invalid schedule ID.");
            }

            try
            {
                var schedule = await _scheduleAppService.GetScheduleByIdAsync(id);
                if (schedule == null)
                {
                    return ResponseHelper.NotFound<ScheduleResponseDto>("Schedule not found.");
                }

                return ResponseHelper.Success(schedule, "Schedule retrieved successfully.");
            }
            catch (Exception ex)
            {
                return ResponseHelper.Error<ScheduleResponseDto>($"An error occurred: {ex.Message}");
            }
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
                return ResponseHelper.BadRequest<int>("Schedule data is required.");
            }

            try
            {
                var createdScheduleId = await _scheduleAppService.CreateScheduleAsync(new CreateScheduleCommand(scheduleDto));
                return ResponseHelper.Success(createdScheduleId, "Schedule created successfully.");
            }
            catch (Exception ex)
            {
                return ResponseHelper.Error<int>($"An error occurred: {ex.Message}");
            }
        }

        /// <summary>
        /// Updates an existing schedule.
        /// </summary>
        /// <param name="id">The ID of the schedule to update.</param>
        /// <param name="scheduleDto">The updated details of the schedule.</param>
        /// <returns>A success message if the update is successful.</returns>
        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResponse<string>>> UpdateSchedule(int id, [FromBody] ScheduleResquestDto scheduleDto)
        {
            if (id <= 0 || scheduleDto == null)
            {
                return ResponseHelper.BadRequest<string>("Invalid schedule ID or data.");
            }

            try
            {
                var existingSchedule = await _scheduleAppService.GetScheduleByIdAsync(id);
                if (existingSchedule == null)
                {
                    return ResponseHelper.NotFound<string>("Schedule not found.");
                }

                await _scheduleAppService.UpdateScheduleAsync(new UpdateScheduleCommand(id, scheduleDto));
                return ResponseHelper.Success<string>(null, "Schedule updated successfully.");
            }
            catch (Exception ex)
            {
                return ResponseHelper.Error<string>($"An error occurred: {ex.Message}");
            }
        }

        /// <summary>
        /// Deletes a schedule by its ID.
        /// </summary>
        /// <param name="id">The ID of the schedule to delete.</param>
        /// <returns>A success message if the deletion is successful.</returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse<string>>> DeleteSchedule(int id)
        {
            if (id <= 0)
            {
                return ResponseHelper.BadRequest<string>("Invalid schedule ID.");
            }

            try
            {
                var existingSchedule = await _scheduleAppService.GetScheduleByIdAsync(id);
                if (existingSchedule == null)
                {
                    return ResponseHelper.NotFound<string>("Schedule not found.");
                }

                await _scheduleAppService.DeleteScheduleAsync(id);
                return ResponseHelper.Success<string>(null, "Schedule deleted successfully.");
            }
            catch (Exception ex)
            {
                return ResponseHelper.Error<string>($"An error occurred: {ex.Message}");
            }
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
                return ResponseHelper.BadRequest<IEnumerable<ScheduleResponseDto>>("Invalid specialty ID.");
            }

            try
            {
                var schedules = await _scheduleAppService.GetSchedulesBySpecialtyAsync(specialtyId);
                if (schedules == null || !schedules.Any())
                {
                    return ResponseHelper.NotFound<IEnumerable<ScheduleResponseDto>>("No schedules found for this specialty.");
                }

                return ResponseHelper.Success(schedules, "Schedules by specialty retrieved successfully.");
            }
            catch (Exception ex)
            {
                return ResponseHelper.Error<IEnumerable<ScheduleResponseDto>>($"An error occurred: {ex.Message}");
            }
        }
    }
}