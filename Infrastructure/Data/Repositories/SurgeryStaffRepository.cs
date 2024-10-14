using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repositories;

public class SurgeryStaffRepository : BaseRepository<SurgeryStaff>, ISurgeryStaffRepository
{
    public SurgeryStaffRepository(ApplicationDbContext context) : base(context) { }

    public async Task<IEnumerable<SurgeryStaff>> GetStaffBySurgeryIdAsync(int surgeryId)
    {
        return await _context.SurgeryStaffs
            .Where(ss => ss.SurgeryId == surgeryId)
            .ToListAsync();
    }
}