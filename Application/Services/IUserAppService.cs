using Application.Commands.Users;
using Application.Models.ReponseDtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Services
{
    /// <summary>
    /// Defines the contract for managing user-related operations in the application.
    /// This interface provides methods to create, update, delete, and retrieve users.
    /// </summary>
    public interface IUserAppService
    {
        /// <summary>
        /// Creates a new user in the system.
        /// </summary>
        /// <param name="command">The command containing the user data to be created.</param>
        /// <returns>The ID of the newly created user.</returns>
        Task<int> CreateUserAsync(CreateUserCommand command);

        /// <summary>
        /// Updates an existing user in the system.
        /// </summary>
        /// <param name="command">The command containing the updated user data.</param>
        /// <returns>A task representing the asynchronous update operation.</returns>
        Task UpdateUserAsync(UpdateUserCommand command);

        /// <summary>
        /// Deletes a user from the system by their ID.
        /// </summary>
        /// <param name="command">The command containing the ID of the user to delete.</param>
        /// <returns>A task representing the asynchronous delete operation.</returns>
        Task DeleteUserAsync(DeleteUserCommand command);

        /// <summary>
        /// Retrieves all users in the system.
        /// </summary>
        /// <returns>A collection of UserResponseDto representing all users.</returns>
        Task<IEnumerable<UserResponseDto>> GetAllUsersAsync();

        /// <summary>
        /// Retrieves a specific user by their ID.
        /// </summary>
        /// <param name="id">The ID of the user to retrieve.</param>
        /// <returns>A UserResponseDto representing the requested user.</returns>
        Task<UserResponseDto> GetUserByIdAsync(int id);

        Task<IEnumerable<UserResponseDto>> GetUsersByStateAsync(int stateId);
    }
}