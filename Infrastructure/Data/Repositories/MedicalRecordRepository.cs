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
        /// Retrieves the list of medical records for a given patient ID, including related Patient and Staff entities.
        /// </summary>
        /// <param name="patientId">The ID of the patient whose medical records to retrieve.</param>
        /// <returns>A list of medical records with related Patient and Staff, or an empty list if none are found.</returns>
        public async Task<List<MedicalRecord>> GetMedicalRecordByPatientIdAsync(int patientId)
        {
            return await _context.MedicalRecords
                .Include(mr => mr.Patient)  // Include the related Patient entity
                .Include(mr => mr.Staff)    // Include the related Staff entity
                .Where(mr => mr.PatientId == patientId)  // Filter by patient ID
                .ToListAsync(); // Convert the query result to a list
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