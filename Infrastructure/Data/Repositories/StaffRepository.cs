using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repositories;

public class StaffRepository : BaseRepository<Staff>, IStaffRepository
{
    public StaffRepository(ApplicationDbContext context) : base(context) { }

    public async Task<Staff> GetStaffByUserIdAsync(int userId)
    {
        return await _context.Staffs
            .FirstOrDefaultAsync(s => s.UserId == userId);
    }

    public async Task<IEnumerable<Staff>> GetStaffBySpecialtyIdAsync(int specialtyId)
    {
        return await _context.Staffs
            .Where(s => s.SpecialtyId == specialtyId)
            .ToListAsync();
    }
}