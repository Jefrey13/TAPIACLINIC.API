using Application.Commands.Auth;
using Application.Models.RequestDtos;
using Application.Services;
using API.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace API.Controllers
{
    /// <summary>
    /// Controller for handling authentication-related actions such as login, token refresh, and password changes.
    /// </summary>
    [Authorize]
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
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult<ApiResponse<object>>> Login([FromBody] LoginRequestDto request)
        {
            var response = await _authService.LoginAsync(new LoginCommand(request));
            var apiResponse = new ApiResponse<object>(true, "Login successful", response, 200);
            return Ok(apiResponse);
        }

        /// <summary>
        /// Refreshes the user's access token using a valid refresh token.
        /// </summary>
        /// <param name="request">The refresh token request.</param>
        /// <returns>A new access token if the refresh was successful.</returns>
        [HttpPost("refresh-token")]
        [AllowAnonymous]
        public async Task<ActionResult<ApiResponse<object>>> RefreshToken([FromBody] RefreshTokenRequestDto request)
        {
            var response = await _authService.RefreshTokenAsync(new RefreshTokenCommand(request));
            var apiResponse = new ApiResponse<object>(true, "Token refreshed successfully", response, 200);
            return Ok(apiResponse);
        }

        /// <summary>
        /// Changes the user's password.
        /// </summary>
        /// <param name="request">The request containing the old and new password.</param>
        /// <returns>A success message if the password was changed successfully.</returns>
        [HttpPost("change-password")]
        public async Task<ActionResult<ApiResponse<string>>> ChangePassword([FromBody] ChangePasswordRequestDto request)
        {
            var jwtToken = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            var result = await _authService.ChangePasswordAsync(new ChangePasswordCommand(request), jwtToken);

            if (result)
            {
                var successResponse = new ApiResponse<string>(true, "Password changed successfully", null, 200);
                return Ok(successResponse);
            }

            var errorResponse = new ApiResponse<string>(false, "Error changing password", null, 400);
            return BadRequest(errorResponse);
        }
    }
}