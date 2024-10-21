using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repositories;

public class ScheduleRepository : BaseRepository<Schedule>, IScheduleRepository
{
    public ScheduleRepository(ApplicationDbContext context) : base(context) { }

    //public async Task<IEnumerable<Specialty>> GetSchedulesBySpecialtyAsync(int specialtyId)
    //{
    //    return await _context.Schedules
    //        .Where(s => s.SpecialtyId == specialtyId)
    //        .ToListAsync();
    //}
}