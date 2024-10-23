using Application.Commands.Auth;
using Application.Models.RequestDtos;
using Application.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace API.Controllers
{
    /// <summary>
    /// Controller for handling authentication-related actions such as login, token refresh, and password changes.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthController"/> class.
        /// </summary>
        /// <param name="authService">The authentication service.</param>
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        /// <summary>
        /// Logs in a user and returns access and refresh tokens.
        /// </summary>
        /// <param name="request">The login request containing username and password.</param>
        /// <returns>The login response with tokens if successful.</returns>
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto request)
        {
            var response = await _authService.LoginAsync(new LoginCommand(request));
            return Ok(response);
        }

        /// <summary>
        /// Refreshes the user's access token using a valid refresh token.
        /// </summary>
        /// <param name="request">The refresh token request.</param>
        /// <returns>A new access token if the refresh was successful.</returns>
        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequestDto request)
        {
            var response = await _authService.RefreshTokenAsync(new RefreshTokenCommand(request));
            return Ok(response);
        }

        /// <summary>
        /// Changes the user's password.
        /// </summary>
        /// <param name="request">The request containing the old and new password.</param>
        /// <returns>A success message if the password was changed successfully.</returns>
        [HttpPost("change-password")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordRequestDto request)
        {
            var jwtToken = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            var result = await _authService.ChangePasswordAsync(new ChangePasswordCommand(request), jwtToken);
            if (result)
            {
                return Ok("Password changed successfully.");
            }
            return BadRequest("Error changing password.");
        }
    }
}