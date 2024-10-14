using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repositories;

public class MedicalRecordRepository : BaseRepository<MedicalRecord>, IMedicalRecordRepository
{
    public MedicalRecordRepository(ApplicationDbContext context) : base(context) { }

    public async Task<MedicalRecord> GetMedicalRecordByPatientIdAsync(int patientId)
    {
        return await _context.MedicalRecords
            .FirstOrDefaultAsync(mr => mr.PatientId == patientId);
    }
}