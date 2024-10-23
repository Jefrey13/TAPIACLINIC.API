using Application.Commands.Auth;
using Application.Models.ReponseDtos;
using System.Threading.Tasks;

namespace Application.Services
{
    /// <summary>
    /// Service interface for handling authentication and password management.
    /// </summary>
    public interface IAuthService
    {
        /// <summary>
        /// Logs in a user with the provided credentials.
        /// </summary>
        /// <param name="command">The login command containing the user's credentials.</param>
        /// <returns>The login response DTO containing access and refresh tokens.</returns>
        Task<LoginResponseDto> LoginAsync(LoginCommand command);

        /// <summary>
        /// Refreshes the user's access token.
        /// </summary>
        /// <param name="command">The refresh token command containing the refresh token.</param>
        /// <returns>The login response DTO containing a new access token.</returns>
        Task<LoginResponseDto> RefreshTokenAsync(RefreshTokenCommand command);

        /// <summary>
        /// Verifies that the provided password matches the stored password hash.
        /// </summary>
        /// <param name="inputPassword">The plain text password provided by the user.</param>
        /// <param name="storedHash">The hashed password stored in the database.</param>
        /// <returns>True if the password is valid, otherwise false.</returns>
        bool VerifyPassword(string inputPassword, string storedHash);

        /// <summary>
        /// Changes the user's password.
        /// </summary>
        /// <param name="command">The command containing the new password details.</param>
        /// <param name="jwtToken">The JWT token for extracting the user information.</param>
        /// <returns>True if the password was successfully changed, otherwise false.</returns>
        Task<bool> ChangePasswordAsync(ChangePasswordCommand command, string jwtToken);
    }
}