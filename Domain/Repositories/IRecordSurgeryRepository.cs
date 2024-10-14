using Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Repositories
{
    public interface IRecordSurgeryRepository : IRepository<RecordSurgery>
    {
        /// <summary>
        /// Retrieves all surgeries associated with a specific medical record.
        /// </summary>
        /// <param name="medicalRecordId">The ID of the medical record.</param>
        /// <returns>A collection of surgeries linked to the medical record.</returns>
        Task<IEnumerable<RecordSurgery>> GetSurgeriesByMedicalRecordIdAsync(int medicalRecordId);

        /// <summary>
        /// Retrieves all surgeries linked to a specific consultation.
        /// </summary>
        /// <param name="consultationId">The ID of the consultation.</param>
        /// <returns>A collection of surgeries linked to the consultation.</returns>
        Task<IEnumerable<RecordSurgery>> GetSurgeriesByConsultationIdAsync(int consultationId);
    }
}