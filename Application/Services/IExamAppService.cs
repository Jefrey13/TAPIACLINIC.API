using Application.Commands.Exams;
using Application.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Services
{
    /// <summary>
    /// Defines the contract for managing exam-related operations in the application.
    /// </summary>
    public interface IExamAppService
    {
        /// <summary>
        /// Creates a new exam in the system.
        /// </summary>
        /// <param name="command">The command object containing the details of the exam to be created.</param>
        /// <returns>Returns the ID of the newly created exam.</returns>
        Task<int> CreateExamAsync(CreateExamCommand command);

        /// <summary>
        /// Updates an existing exam.
        /// </summary>
        /// <param name="command">The command object containing updated exam details.</param>
        /// <returns>A Task that represents the asynchronous operation.</returns>
        Task UpdateExamAsync(UpdateExamCommand command);

        /// <summary>
        /// Deletes an exam by its unique ID.
        /// </summary>
        /// <param name="id">The ID of the exam to delete.</param>
        /// <returns>A Task that represents the asynchronous operation.</returns>
        Task DeleteExamAsync(int id);

        /// <summary>
        /// Retrieves all exams.
        /// </summary>
        /// <returns>Returns a list of all exams in the system as DTOs.</returns>
        Task<IEnumerable<ExamDto>> GetAllExamsAsync();

        /// <summary>
        /// Retrieves an exam by its unique ID.
        /// </summary>
        /// <param name="id">The ID of the exam to retrieve.</param>
        /// <returns>Returns the ExamDto object of the exam with the given ID.</returns>
        Task<ExamDto> GetExamByIdAsync(int id);
    }
}