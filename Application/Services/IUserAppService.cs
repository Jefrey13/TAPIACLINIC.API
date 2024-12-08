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
        /// <returns>Returns true if the user was created successfully.</returns>
        Task<bool> CreateUserAsync(CreateUserCommand command, string recaptchaToken);

        Task<bool> CreateUserReceptionistAsync(CreateUserCommand command);
        /// <summary>
        /// Updates an existing user in the system.
        /// </summary>
        /// <param name="command">The command containing the updated user data.</param>
        /// <returns>Returns true if the user was updated successfully.</returns>
        Task<bool> UpdateUserAsync(UpdateUserCommand command);

        /// <summary>
        /// Updates the account activation status of a user based on their email.
        /// </summary>
        /// <param name="command">The command containing the updated user data.</param>
        /// <returns>Returns true if the user was updated successfully.</returns>
        Task<bool> UpdateUserIsAccountActivatedAsync(UpdateUserIsAccountActivatedCommand command);

        /// <summary>
        /// Deletes a user from the system by their ID.
        /// </summary>
        /// <param name="command">The command containing the ID of the user to delete.</param>
        /// <returns>Returns true if the user was deleted successfully.</returns>
        Task<bool> DeleteUserAsync(DeleteUserCommand command);

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