using Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Repositories
{
    public interface IPrescriptionRepository : IRepository<Prescription>
    {
        /// <summary>
        /// Retrieve all prescriptions for a specific patient by their ID.
        /// </summary>
        /// <param name="patientId">The ID of the patient.</param>
        /// <returns>A list of prescriptions for the specified patient.</returns>
        Task<IEnumerable<Prescription>> GetPrescriptionsByPatientIdAsync(int patientId);
    }
}