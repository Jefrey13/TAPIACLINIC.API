using Application.Commands.Auth;
using Application.Models.RequestDtos;
using Application.Services;
using API.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

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
        /// Inicia sesión para un usuario y devuelve los tokens de acceso y actualización.
        /// </summary>
        /// <param name="request">La solicitud de inicio de sesión que contiene nombre de usuario y contraseña.</param>
        /// <returns>La respuesta del inicio de sesión con los tokens si es exitoso.</returns>
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult<ApiResponse<object>>> Login([FromBody] LoginRequestDto request)
        {
            if (request == null)
            {
                return BadRequest(new ApiResponse<object>(false, "La solicitud de inicio de sesión no puede estar vacía.", null, 400));
            }

            try
            {
                // Invocar el servicio de autenticación con el comando LoginCommand
                var response = await _authService.LoginAsync(new LoginCommand(request));

                // Verificar si la respuesta es válida
                if (response == null)
                {
                    return Unauthorized(new ApiResponse<object>(false, "Credenciales de inicio de sesión inválidas.", null, 401));
                }

                // Retornar respuesta exitosa
                var apiResponse = new ApiResponse<object>(true, "Inicio de sesión exitoso.", response, 200);
                return Ok(apiResponse);
            }
            catch (ValidationException ex)
            {
                // Manejar excepciones de validación
                return BadRequest(new ApiResponse<object>(false, $"Credenciales de inicio de sesión inválidas.: {ex.Message}", null, 400));
            }
            catch (UnauthorizedAccessException ex)
            {
                // Manejar errores de acceso no autorizado
                return Unauthorized(new ApiResponse<object>(false, $"No autorizado: {ex.Message}", null, 401));
            }
            catch (Exception ex)
            {
                // Manejar cualquier otra excepción
                return StatusCode(500, new ApiResponse<object>(false, "Ocurrió un error inesperado.", null, 500)
                {
                    Errors = new Dictionary<string, string> { { "Excepción", ex.Message } }
                });
            }
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