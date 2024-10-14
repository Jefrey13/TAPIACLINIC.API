using Application.Commands.Surgeries;
using Application.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Services
{
    /// <summary>
    /// Service interface for managing surgeries.
    /// </summary>
    public interface ISurgeryAppService
    {
        /// <summary>
        /// Creates a new surgery.
        /// </summary>
        /// <param name="command">The command containing details like surgery name, description, and state ID.</param>
        /// <returns>The ID of the newly created surgery.</returns>
        Task<int> CreateSurgeryAsync(CreateSurgeryCommand command);

        /// <summary>
        /// Updates an existing surgery.
        /// </summary>
        /// <param name="command">The command with updated surgery details.</param>
        Task UpdateSurgeryAsync(UpdateSurgeryCommand command);

        /// <summary>
        /// Deletes a surgery by its unique ID.
        /// </summary>
        /// <param name="id">The ID of the surgery to delete.</param>
        Task DeleteSurgeryAsync(int id);

        /// <summary>
        /// Retrieves all surgeries.
        /// </summary>
        /// <returns>A list of all surgeries as DTOs.</returns>
        Task<IEnumerable<SurgeryDto>> GetAllSurgeriesAsync();

        /// <summary>
        /// Retrieves a surgery by its unique ID.
        /// </summary>
        /// <param name="id">The ID of the surgery to retrieve.</param>
        /// <returns>The DTO of the surgery.</returns>
        Task<SurgeryDto> GetSurgeryByIdAsync(int id);
    }
}