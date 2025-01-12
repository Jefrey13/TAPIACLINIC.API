using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Data.Repositories
{
    public class PrescriptionRepository : BaseRepository<Prescription>, IPrescriptionRepository
    {
        public PrescriptionRepository(ApplicationDbContext context) : base(context) { }

        /// <summary>
        /// Retrieves a prescription by its ID, including related Patient and Doctor entities.
        /// </summary>
        /// <param name="id">The ID of the prescription to retrieve.</param>
        /// <returns>The prescription with related Patient and Doctor, or null if not found.</returns>
        public override async Task<Prescription> GetByIdAsync(int id)
        {
            return await _context.Prescriptions
                .Include(p => p.Patient)  // Include the related Patient entity
                .Include(p => p.Doctor)   // Include the related Doctor entity
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        /// <summary>
        /// Retrieves all prescriptions for a specific patient by their ID, including related Patient and Doctor entities.
        /// </summary>
        /// <param name="patientId">The ID of the patient.</param>
        /// <returns>A list of prescriptions for the specified patient.</returns>
        public async Task<IEnumerable<Prescription>> GetPrescriptionsByPatientIdAsync(int patientId)
        {
            return await _context.Prescriptions
                .Include(p => p.Patient)  // Include the related Patient entity
                .Include(p => p.Doctor)   // Include the related Doctor entity
                .Where(p => p.PatientId == patientId)
                .ToListAsync();
        }

        /// <summary>
        /// Retrieves all prescriptions, including related Patient and Doctor entities.
        /// </summary>
        /// <returns>A list of prescriptions with related Patient and Doctor.</returns>
        public override async Task<IEnumerable<Prescription>> GetAllAsync()
        {
            return await _context.Prescriptions
                .Include(p => p.Patient)  // Include the related Patient entity
                .Include(p => p.Doctor)   // Include the related Doctor entity
                .ToListAsync();
        }
    }
}
