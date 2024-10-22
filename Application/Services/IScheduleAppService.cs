using Application.Commands;
using Application.Commands.Schedules;
using Application.Models.ReponseDtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Services
{
    /// <summary>
    /// Service interface for managing schedules.
    /// </summary>
    public interface IScheduleAppService
    {
        /// <summary>
        /// Creates a new schedule.
        /// </summary>
        /// <param name="command">The command containing schedule details like specialty ID and time slots.</param>
        /// <returns>The ID of the newly created schedule.</returns>
        Task<int> CreateScheduleAsync(CreateScheduleCommand command);

        /// <summary>
        /// Updates an existing schedule.
        /// </summary>
        /// <param name="command">The command with updated schedule information.</param>
        Task UpdateScheduleAsync(UpdateScheduleCommand command);

        /// <summary>
        /// Deletes a schedule by its unique ID.
        /// </summary>
        /// <param name="id">The ID of the schedule to delete.</param>
        Task DeleteScheduleAsync(int id);

        /// <summary>
        /// Retrieves all schedules.
        /// </summary>
        /// <returns>A list of all schedules as DTOs.</returns>
        Task<IEnumerable<ScheduleResponseDto>> GetAllSchedulesAsync();

        /// <summary>
        /// Retrieves a schedule by its unique ID.
        /// </summary>
        /// <param name="id">The ID of the schedule to retrieve.</param>
        /// <returns>The DTO of the schedule.</returns>
        Task<ScheduleResponseDto> GetScheduleByIdAsync(int id);

        /// <summary>
        /// Retrieves schedules associated with a specific specialty.
        /// </summary>
        /// <param name="specialtyId">The ID of the specialty to filter schedules.</param>
        /// <returns>A list of schedules associated with the specialty.</returns>
        Task<IEnumerable<ScheduleResponseDto>> GetSchedulesBySpecialtyAsync(int specialtyId);
    }
}