using Application.Commands.States;
using Application.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Services
{
    /// <summary>
    /// Defines the contract for managing state-related operations in the application.
    /// </summary>
    public interface IStateAppService
    {
        /// <summary>
        /// Creates a new state.
        /// </summary>
        Task<int> CreateStateAsync(CreateStateCommand command);

        /// <summary>
        /// Updates an existing state.
        /// </summary>
        Task UpdateStateAsync(UpdateStateCommand command);

        /// <summary>
        /// Deletes a state by its ID.
        /// </summary>
        Task DeleteStateAsync(DeleteStateCommand command);

        /// <summary>
        /// Retrieves all states.
        /// </summary>
        Task<IEnumerable<StateDto>> GetAllStatesAsync();

        /// <summary>
        /// Retrieves a state by its ID.
        /// </summary>
        Task<StateDto> GetStateByIdAsync(int id);
    }
}