using Application.Commands.Specialties;
using Application.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Services
{
    /// <summary>
    /// Service interface for managing medical specialties.
    /// </summary>
    public interface ISpecialtyAppService
    {
        /// <summary>
        /// Creates a new medical specialty.
        /// </summary>
        /// <param name="command">The command containing details like name and description.</param>
        /// <returns>The ID of the newly created specialty.</returns>
        Task<int> CreateSpecialtyAsync(CreateSpecialtyCommand command);

        /// <summary>
        /// Updates an existing medical specialty.
        /// </summary>
        /// <param name="command">The command with updated specialty details.</param>
        Task UpdateSpecialtyAsync(UpdateSpecialtyCommand command);

        /// <summary>
        /// Deletes a specialty by its unique ID.
        /// </summary>
        /// <param name="id">The ID of the specialty to delete.</param>
        Task DeleteSpecialtyAsync(int id);

        /// <summary>
        /// Retrieves all specialties.
        /// </summary>
        /// <returns>A list of all specialties as DTOs.</returns>
        Task<IEnumerable<SpecialtyDto>> GetAllSpecialtiesAsync();

        /// <summary>
        /// Retrieves a specialty by its unique ID.
        /// </summary>
        /// <param name="id">The ID of the specialty to retrieve.</param>
        /// <returns>The DTO of the specialty.</returns>
        Task<SpecialtyDto> GetSpecialtyByIdAsync(int id);
    }
}