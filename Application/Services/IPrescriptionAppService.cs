using Application.Commands.Prescriptions;
using Application.Models.ResponseDtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Services
{
    /// <summary>
    /// Defines the contract for managing prescription operations in the application.
    /// This interface provides methods to create, update, delete, and retrieve prescriptions.
    /// </summary>
    public interface IPrescriptionAppService
    {
        /// <summary>
        /// Creates a new prescription in the system.
        /// </summary>
        /// <param name="command">The command containing the prescription data to be created.</param>
        /// <returns>The ID of the newly created prescription.</returns>
        Task<int> CreatePrescriptionAsync(CreatePrescriptionCommand command);

        /// <summary>
        /// Retrieves all prescriptions in the system.
        /// </summary>
        /// <returns>A collection of PrescriptionResponseDto representing all prescriptions.</returns>
        Task<IEnumerable<PrescriptionResponseDto>> GetAllPrescriptionsAsync();

        /// <summary>
        /// Retrieves a specific prescription by its ID.
        /// </summary>
        /// <param name="id">The ID of the prescription to retrieve.</param>
        /// <returns>A PrescriptionResponseDto representing the requested prescription.</returns>
        Task<PrescriptionResponseDto> GetPrescriptionByIdAsync(int id);

        /// <summary>
        /// Retrieves prescriptions by the patient's ID.
        /// </summary>
        /// <param name="patientId">The ID of the patient whose prescriptions are being retrieved.</param>
        /// <returns>A collection of PrescriptionResponseDto representing the prescriptions for the specified patient.</returns>
        Task<IEnumerable<PrescriptionResponseDto>> GetPrescriptionsByPatientIdAsync(int patientId);

        //string RenderPrescriptionToHtml(PrescriptionResponseDto prescription);

        byte[] GeneratePdf(PrescriptionResponseDto prescription);
    }
}