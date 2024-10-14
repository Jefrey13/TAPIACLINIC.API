using Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Repositories
{
    public interface IConsultationExamRepository : IRepository<ConsultationExam>
    {
        /// <summary>
        /// Retrieves all exams associated with a specific consultation.
        /// </summary>
        /// <param name="consultationId">The ID of the consultation.</param>
        /// <returns>A collection of exams linked to the consultation.</returns>
        Task<IEnumerable<ConsultationExam>> GetExamsByConsultationIdAsync(int consultationId);

        /// <summary>
        /// Retrieves all consultations associated with a specific exam.
        /// </summary>
        /// <param name="examId">The ID of the exam.</param>
        /// <returns>A collection of consultations linked to the exam.</returns>
        Task<IEnumerable<ConsultationExam>> GetConsultationsByExamIdAsync(int examId);
    }
}