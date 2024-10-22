using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repositories
{
    /// <summary>
    /// Repository for managing Schedule entities and their relations.
    /// </summary>
    public class ScheduleRepository : BaseRepository<Schedule>, IScheduleRepository
    {
        public ScheduleRepository(ApplicationDbContext context) : base(context) { }

        /// <summary>
        /// Retrieves all schedules associated with a specific specialty.
        /// </summary>
        /// <param name="specialtyId">The ID of the specialty.</param>
        /// <returns>A collection of schedules related to the specialty.</returns>
        public async Task<IEnumerable<Schedule>> GetSchedulesBySpecialtyAsync(int specialtyId)
        {
            return await _context.Schedules
                .Include(s => s.Specialty) // Includes full Specialty details
                .Where(s => s.SpecialtyId == specialtyId)
                .ToListAsync();
        }

        /// <summary>
        /// Adds a new schedule to the database.
        /// </summary>
        /// <param name="schedule">The schedule entity to be added.</param>
        public async Task AddAsync(Schedule schedule)
        {
            await _context.Schedules.AddAsync(schedule);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Updates an existing schedule.
        /// </summary>
        /// <param name="schedule">The schedule entity to be updated.</param>
        public async Task UpdateAsync(Schedule schedule)
        {
            _context.Schedules.Update(schedule);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Deletes a schedule from the database.
        /// </summary>
        /// <param name="schedule">The schedule entity to be deleted.</param>
        public async Task DeleteAsync(Schedule schedule)
        {
            _context.Schedules.Remove(schedule);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Retrieves a schedule by its ID, including its associated specialty.
        /// </summary>
        /// <param name="id">The ID of the schedule.</param>
        /// <returns>The schedule with the given ID.</returns>
        public async Task<Schedule> GetByIdAsync(int id)
        {
            return await _context.Schedules
                .Include(s => s.Specialty) // Includes full Specialty details
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        /// <summary>
        /// Retrieves all schedules from the database.
        /// </summary>
        /// <returns>A collection of all schedules.</returns>
        public async Task<IEnumerable<Schedule>> GetAllAsync()
        {
            return await _context.Schedules
                .Include(s => s.Specialty) // Includes full Specialty details
                .ToListAsync();
        }
    }
}