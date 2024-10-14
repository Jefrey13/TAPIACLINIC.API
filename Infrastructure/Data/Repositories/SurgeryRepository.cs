using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repositories;

public class SurgeryRepository : BaseRepository<Surgery>, ISurgeryRepository
{
    public SurgeryRepository(ApplicationDbContext context) : base(context) { }

    public async Task<IEnumerable<Surgery>> GetSurgeriesByPatientIdAsync(int patientId)
    {
        return await _context.Surgeries
            .Where(s => s.RecordSurgeries.Any(rs => rs.MedicalRecord.PatientId == patientId))
            .ToListAsync();
    }
}