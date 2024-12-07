using Application.Commands.Auth;
using Application.Models.RequestDtos;
using Application.Services;
using API.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Application.Commands.Users;
using Application.Models.ReponseDtos;
using Application.Models.RequestDtos.UpdateRequestDto;
using Application.Services.Impl;
using Application.Models.ResponseDtos;
using Sprache;

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
        private readonly IUserAppService _userAppService;

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
        /// <param name="recaptchaToken">El token de reCAPTCHA proporcionado en el encabezado de la solicitud.</param>
        /// <returns>La respuesta del inicio de sesión con los tokens si es exitoso.</returns>
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult<ApiResponse<LoginResponseDto>>> Login(
            [FromBody] LoginRequestDto request,
            [FromHeader(Name = "RecaptchaToken")] string recaptchaToken)
        {
            // Validar si el modelo de datos es válido
            if (!ModelState.IsValid)
            {
                var errorMessages = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                // Respuesta uniforme con ResponseHelper
                return ResponseHelper.BadRequest<LoginResponseDto>(string.Join("; ", errorMessages));
            }

            // Validar el token de reCAPTCHA
            if (string.IsNullOrEmpty(recaptchaToken))
            {
                return ResponseHelper.BadRequest<LoginResponseDto>("El token de reCAPTCHA es obligatorio.");
            }

            try
            {
                // Invocar el servicio de autenticación con el comando LoginCommand
                var loginResponse = await _authService.LoginAsync(new LoginCommand(request), recaptchaToken);

                // Verificar si la respuesta es válida
                if (loginResponse == null)
                {
                    return ResponseHelper.Unauthorized<LoginResponseDto>("Credenciales de inicio de sesión inválidas o reCAPTCHA fallido.");
                }

                // Retornar respuesta exitosa directamente con LoginResponseDto
                return ResponseHelper.Success(loginResponse, "Inicio de sesión exitoso.");
            }
            catch (ValidationException ex)
            {
                // Manejar excepciones de validación
                return ResponseHelper.BadRequest<LoginResponseDto>($"Credenciales inválidas: {ex.Message}");
            }
            catch (UnauthorizedAccessException ex)
            {
                // Manejar errores de acceso no autorizado
                return ResponseHelper.Unauthorized<LoginResponseDto>($"No autorizado: {ex.Message}");
            }
            catch (Exception ex)
            {
                // Manejar cualquier otra excepción inesperada
                return ResponseHelper.Error<LoginResponseDto>($"Ocurrió un error inesperado: {ex.Message}");
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

        /// <summary>
        /// Activates a user's account and sends an activation message.
        /// </summary>
        /// <param name="contactRequestDto">The request object containing contact details.</param>
        /// <returns>An API response with the operation result.</returns>
        [HttpPost("SendMessage")]
        [AllowAnonymous]
        public async Task<ActionResult<ApiResponse<ServiceResult>>> SendMessage([FromBody] EmailContactRequestDto request)
        {
            if (!ModelState.IsValid)
            {
                // Extrae los mensajes de error de ModelState
                var errorMessages = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                return ResponseHelper.BadRequest<ServiceResult>(string.Join("; ", errorMessages));
            }

            try
            {
                var activationResult = await _authService.SendMessageAsync(request.Email);

                // Si reCAPTCHA es inválido, retornamos un error
                if (!activationResult.Success)
                {
                    return ResponseHelper.Error<ServiceResult>("reCAPTCHA Invalido. Su intento no fue válido, por favor intente de nuevo.");
                }
                if (activationResult == null)
                {
                    return ResponseHelper.Error<ServiceResult>("Error al crear el usuario");
                }
                if (activationResult == null || !activationResult.Success)
                {
                    return ResponseHelper.NotFound<ServiceResult>("Usuario no encontrado o activación fallida.");
                }

                return ResponseHelper.Success(activationResult, "El mensaje de activación se envió correctamente.");
            }
            catch (Exception ex)
            {
                // Log exception (assumes a logging mechanism exists)
                //_logger.LogError(ex, "An error occurred during account activation.");

                return ResponseHelper.Error<ServiceResult>($"Ocurrió un error inesperado.Por favor, intente mas tarde.");
            }
        }

        /// <summary>
        /// Activates a user's account and sends an activation message.
        /// </summary>
        /// <param name="contactRequestDto">The request object containing contact details.</param>
        /// <returns>An API response with the operation result.</returns>
        //[HttpPatch("ActivateAccount")]
        [HttpGet("ActivateAccount/{token}")]
        [AllowAnonymous]
        public async Task<ActionResult<ApiResponse<ServiceResult>>> ActivateAccount(string token)
        {
            if (string.IsNullOrEmpty(token))
            {
                return ResponseHelper.BadRequest<ServiceResult>("El token de activacion no ha sido proporcionado.");
            }

            try
            {
                var activationResult = await _authService.ActivateAccount(token);

                if (activationResult == null || !activationResult.Success)
                {
                    return ResponseHelper.NotFound<ServiceResult>("Usuario no encontrado o activación fallida.");
                }

                return ResponseHelper.Success(activationResult, "El mensaje de activación se envió correctamente.");
            }
            catch (Exception ex)
            {
                // Log exception (assumes a logging mechanism exists)
                //_logger.LogError(ex, "An error occurred during account activation.");

                return ResponseHelper.Error<ServiceResult>($"Ocurrió un error inesperado. Por favor, intente mas tarde.");
            }
        }

    }
}