using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repositories;

public class SpecialtyRepository : BaseRepository<Specialty>, ISpecialtyRepository
{
    public SpecialtyRepository(ApplicationDbContext context) : base(context) { }

    public async Task<IEnumerable<Specialty>> GetSpecialtiesByStaffIdAsync(int staffId)
    {
        return await _context.Specialties
            .Where(s => s.Staffs.Any(st => st.Id == staffId))
            .ToListAsync();
    }
}