using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repositories;

public class ScheduleRepository : BaseRepository<Schedule>, IScheduleRepository
{
    public ScheduleRepository(ApplicationDbContext context) : base(context) { }

    public async Task<IEnumerable<Schedule>> GetSchedulesByStaffIdAsync(int staffId)
    {
        return await _context.Schedules
            .Where(s => s.StaffId == staffId)
            .ToListAsync();
    }
}