using Application.Commands.Auth;
using Application.Models.ReponseDtos;
using MediatR;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Domain.Repositories;
using Application.Services;
using Application.Models.RequestDtos;
using Domain.Entities;
using Application.Models.ResponseDtos;
using Microsoft.IdentityModel.Tokens;
using Application.Commands.Users;
using Application.Queries.Users;

namespace Application.Services.Impl
{
    /// <summary>
    /// Service implementation for handling authentication and password management.
    /// </summary>
    public class AuthService : IAuthService
    {
        private readonly IMediator _mediator;
        private readonly IEmailSender _emailSender;
        private readonly IJwtTokenService _jwtTokenService;
        private readonly IUserRepository _userRepository;
        private readonly IUserAppService _userAppService;


        /// <summary>
        /// Initializes a new instance of the <see cref="AuthService"/> class.
        /// </summary>
        /// <param name="mediator">The mediator for handling commands and queries.</param>
        /// <param name="emailSender">The service for sending emails.</param>
        /// <param name="jwtTokenService">The service for handling JWT tokens.</param>
        /// <param name="userRepository">The repository for user-related data.</param>
        public AuthService(IMediator mediator, IEmailSender emailSender, IJwtTokenService jwtTokenService, IUserRepository userRepository, IUserAppService userAppService)
        {
            _mediator = mediator;
            _emailSender = emailSender;
            _jwtTokenService = jwtTokenService;
            _userRepository = userRepository;
            _userAppService = userAppService;
        }

        /// <inheritdoc />
        public async Task<LoginResponseDto> LoginAsync(LoginCommand command, string recaptchaToken)
        {
            // Verify the reCAPTCHA token before proceeding
            var isRecaptchaValid = await _mediator.Send(new VerifyRecaptchaCommand(recaptchaToken));
            if (!isRecaptchaValid)
            {
                return null; // Retorna null si reCAPTCHA no es válido
            }

            // Continuar con el comando de autenticación
            return await _mediator.Send(command);
        }

        /// <inheritdoc />
        public async Task<LoginResponseDto> RefreshTokenAsync(RefreshTokenCommand command)
        {
            return await _mediator.Send(command);
        }

        /// <inheritdoc />
        public bool VerifyPassword(string inputPassword, string storedHash)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashedPassword = Convert.ToBase64String(sha256.ComputeHash(Encoding.UTF8.GetBytes(inputPassword)));
                return hashedPassword == storedHash;
            }
        }

        /// <inheritdoc />
        public async Task<bool> ChangePasswordAsync(ChangePasswordCommand command, string jwtToken)
        {
            // Extract the username from the JWT token
            var username = _jwtTokenService.GetUsernameFromToken(jwtToken);
            var user = await _userRepository.GetUserByUserNameAsync(username);

            if (user == null)
            {
                throw new Exception("User not found.");
            }

            // Change the password
            var result = await _mediator.Send(command);
            if (result)
            {
                // Send notification email
                string emailBody = "<h1>Password Changed</h1><p>Your password has been successfully updated.</p>";
                await _emailSender.SendEmailAsync(user.Email.ToString(), "Password Changed", emailBody);
            }

            return result;
        }

        /// <summary>
        /// Sends an activation message to a specified recipient.
        /// </summary>
        /// <param name="contactRequestDto">The request object containing contact details.</param>
        /// <returns>A task representing the operation result.</returns>
        public async Task<ServiceResult> SendMessageAsync(string email)
        {
            // Validate that the email is provided
            if (string.IsNullOrEmpty(email))
            {
                return new ServiceResult
                {
                    Success = false,
                    Message = "El correo electrónico es obligatorio para enviar el mensaje de activación."
                };
            }

            try
            {

               // Generar el token JWT codificado
               var jwtToken = _jwtTokenService.GenerateActivationToken(email);
                // Convertir a Base64URL
                var base64UrlToken = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(jwtToken));

                //Construir el cuerpo del correo
                string emailBody = $@"
                    <h1>Activación de Cuenta</h1>
                    <p>Hola,</p>
                    <p>Por favor, haz clic en el enlace a continuación para activar tu cuenta:</p>
                    <a href='http://localhost:5073/ActivationSuccess/{base64UrlToken}'>Activar Cuenta</a>";

                // Enviar el correo electrónico
                await _emailSender.SendEmailAsync(email, "Activación de Cuenta", emailBody);


                return new ServiceResult
                {
                    Success = true,
                    Message = "El correo electrónico de activación ha sido enviado correctamente."
                };
            }
            catch (Exception ex)
            {
                // Log the exception for debugging purposes
                //_logger?.LogError(ex, "Ocurrió un error al enviar el correo electrónico de activación.");

                return new ServiceResult
                {
                    Success = false,
                    Message = $"No se pudo enviar el correo de activación: {ex.Message}"
                };
            }
        }

        /// <summary>
        /// Activates a user account based on the provided token.
        /// </summary>
        /// <param name="token">The JWT token used for account activation.</param>
        /// <returns>A task representing the operation result.</returns>
        /// <exception cref="ArgumentNullException">Thrown if the token is null or empty.</exception>
        /// <exception cref="SecurityTokenException">Thrown if the token is invalid or expired.</exception>
        public async Task<ServiceResult> ActivateAccount(string token)
        {
            // Decodificar el token
            //var jwtToken = Uri.UnescapeDataString(token);
            //// Validate that the token is provided
            //if (string.IsNullOrEmpty(jwtToken))
            //{
            //    throw new ArgumentNullException(nameof(jwtToken), "Parece que falta el token. Por favor, verifica el enlace de activación.");
            //}
            var decodedToken = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(token));

            try
            {
                // Extract the principal from the token
                var principal = _jwtTokenService.ValidateActivationToken(decodedToken);

                if (principal == null)
                {
                    return new ServiceResult
                    {
                        Success = false,
                        Message = "El enlace de activación no es válido o ha expirado. Por favor, solicita un nuevo enlace."
                    };
                }

                var userEmail = _jwtTokenService.GetEmailFromToken(decodedToken);

                if (userEmail == null)
                {
                    //Exeption
                    return new ServiceResult
                    {
                        Success = false,
                        Message = "¡Tu cuenta no ha sido activada! Intentelo mas tarde."
                    };
                }
                else
                {
                    await _userAppService.UpdateUserIsAccountActivatedAsync(new UpdateUserIsAccountActivatedCommand(userEmail));

                    return new ServiceResult
                    {
                        Success = true,
                        Message = "¡Tu cuenta ha sido activada con éxito! Ahora puedes iniciar sesión."
                    };
                }
            }

            catch (SecurityTokenException ex)
            {
                // Log the exception
               // _logger?.LogError(ex, "Invalid token provided during account activation.")
               
               //Agregar pro favor el metodo para actualizar los campos.

                return new ServiceResult
                {
                    Success = false,
                    Message = "El enlace de activación no es válido. Por favor, solicita un nuevo enlace y vuelve a intentarlo."
                };
            }
            catch (Exception ex)
            {
                // Log unexpected exceptions
                //_logger?.LogError(ex, "An error occurred while activating the account.");

                return new ServiceResult
                {
                    Success = false,
                    Message = "Ocurrió un error inesperado al intentar activar tu cuenta. Por favor, inténtalo más tarde."
                };
            }
        }

        public async Task<UserResponseDto> GetUsersByUsernameAsync(string token)
        {
            string username = _jwtTokenService.GetUsernameFromToken(token);

            return await _mediator.Send(new GetUsersByUsernameQuery(username));
        }
    }
}