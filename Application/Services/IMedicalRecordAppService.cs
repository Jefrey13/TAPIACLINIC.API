using Application.Commands.MedicalRecords;
using Application.Models.ReponseDtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Services
{
    /// <summary>
    /// Defines the contract for managing medical record operations in the application.
    /// This interface provides methods to create, update, delete, and retrieve medical records.
    /// </summary>
    public interface IMedicalRecordAppService
    {
        /// <summary>
        /// Creates a new medical record in the system.
        /// </summary>
        /// <param name="command">The command containing the medical record data to be created.</param>
        /// <returns>The ID of the newly created medical record.</returns>
        Task<int> CreateMedicalRecordAsync(CreateMedicalRecordCommand command, string jwtToken);

        /// <summary>
        /// Updates an existing medical record in the system.
        /// </summary>
        /// <param name="command">The command containing the updated medical record data.</param>
        /// <returns>A task representing the asynchronous update operation.</returns>
        Task UpdateMedicalRecordAsync(UpdateMedicalRecordCommand command);

        /// <summary>
        /// Deletes a medical record from the system by its ID.
        /// </summary>
        /// <param name="id">The ID of the medical record to delete.</param>
        /// <returns>A task representing the asynchronous delete operation.</returns>
        Task DeleteMedicalRecordAsync(int id);

        /// <summary>
        /// Retrieves all medical records in the system.
        /// </summary>
        /// <returns>A collection of MedicalRecordResponseDto representing all medical records.</returns>
        Task<IEnumerable<MedicalRecordResponseDto>> GetAllMedicalRecordsAsync();

        /// <summary>
        /// Retrieves a specific medical record by its ID.
        /// </summary>
        /// <param name="id">The ID of the medical record to retrieve.</param>
        /// <returns>A MedicalRecordResponseDto representing the requested medical record.</returns>
        Task<MedicalRecordResponseDto> GetMedicalRecordByIdAsync(int id);

        /// <summary>
        /// Retrieves a medical record by the patient's ID.
        /// </summary>
        /// <param name="patientId">The ID of the patient whose medical record is being retrieved.</param>
        /// <returns>A MedicalRecordResponseDto representing the requested medical record for the patient.</returns>
        Task<MedicalRecordResponseDto> GetMedicalRecordByPatientIdAsync(int patientId);
    }
}