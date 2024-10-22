using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repositories
{
    public class MedicalRecordRepository : BaseRepository<MedicalRecord>, IMedicalRecordRepository
    {
        public MedicalRecordRepository(ApplicationDbContext context) : base(context) { }

        /// <summary>
        /// Retrieves a medical record by its ID, including related Patient and Staff entities.
        /// </summary>
        /// <param name="id">The ID of the medical record to retrieve.</param>
        /// <returns>The medical record with related Patient and Staff, or null if not found.</returns>
        public override async Task<MedicalRecord> GetByIdAsync(int id)
        {
            return await _context.MedicalRecords
                .Include(mr => mr.Patient)  // Include the related Patient entity
                .Include(mr => mr.Staff)    // Include the related Staff entity
                .FirstOrDefaultAsync(mr => mr.Id == id);
        }

        /// <summary>
        /// Retrieves a medical record by the patient's ID, including related Patient and Staff entities.
        /// </summary>
        /// <param name="patientId">The ID of the patient whose medical record is being retrieved.</param>
        /// <returns>The medical record with related Patient and Staff, or null if not found.</returns>
        public async Task<MedicalRecord> GetMedicalRecordByPatientIdAsync(int patientId)
        {
            return await _context.MedicalRecords
                .Include(mr => mr.Patient)  // Include the related Patient entity
                .Include(mr => mr.Staff)    // Include the related Staff entity
                .FirstOrDefaultAsync(mr => mr.PatientId == patientId);
        }

        /// <summary>
        /// Retrieves all medical records, including related Patient and Staff entities.
        /// </summary>
        /// <returns>A list of medical records with related Patient and Staff.</returns>
        public override async Task<IEnumerable<MedicalRecord>> GetAllAsync()
        {
            return await _context.MedicalRecords
                .Include(mr => mr.Patient)  // Include the related Patient entity
                .Include(mr => mr.Staff)    // Include the related Staff entity
                .ToListAsync();
        }
    }
}