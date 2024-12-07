using Application.Commands.Users;
using Application.Models.RequestDtos;
using Application.Models.ResponseDtos;
using Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Impl
{
    public class ContactService: IContactService
    {
        private readonly IMediator _mediator;
        private readonly IEmailSender _emailSender;
        private readonly IJwtTokenService _jwtTokenService;
        private readonly IUserRepository _userRepository;

        public ContactService(IMediator mediator, IEmailSender emailSender, IJwtTokenService jwtTokenService, IUserRepository userRepository, IUserAppService userAppService)
        {
            _mediator = mediator;
            _emailSender = emailSender;
            _jwtTokenService = jwtTokenService;
            _userRepository = userRepository;
        }

        /// <summary>
        /// Sends a contact message to the specified email address after verifying reCAPTCHA.
        /// </summary>
        /// <param name="contactRequestDto">The request object containing contact details.</param>
        /// <param name="recaptchaToken">The reCAPTCHA token for validation.</param>
        /// <returns>A service result indicating the success or failure of the operation.</returns>
        public async Task<ServiceResult> SendMessageAsync(ContactRequestDto contactRequestDto, string recaptchaToken)
        {
            // Validate that the email is provided
            if (string.IsNullOrEmpty(contactRequestDto.Email))
            {
                return new ServiceResult
                {
                    Success = false,
                    Message = "Por favor, proporciona un correo electrónico válido para enviar la consulta."
                };
            }

            try
            {
                // Verify the reCAPTCHA token before proceeding
                var isRecaptchaValid = await _mediator.Send(new VerifyRecaptchaCommand(recaptchaToken));
                if (!isRecaptchaValid)
                {
                    return new ServiceResult
                    {
                        Success = false,
                        Message = "La validación de reCAPTCHA falló. Por favor, intenta nuevamente más tarde."
                    };
                }

                // Build the email body
                string emailBody = $@"
                <h1>Consulta de Usuario</h1>
                <p><strong>Nombre:</strong> {contactRequestDto.FullName}</p>
                <p><strong>Correo Electrónico:</strong> {contactRequestDto.Email}</p>
                <p><strong>Teléfono:</strong> {contactRequestDto.PhoneNumber}</p>
                <p><strong>Consulta:</strong> Necesito consultar sobre {contactRequestDto.Consultation}</p>";

                // Send the email
                await _emailSender.SendEmailFromUserAsync(contactRequestDto.Email, "Nueva Consulta de Usuario", emailBody);

                return new ServiceResult
                {
                    Success = true,
                    Message = "La consulta ha sido enviada exitosamente."
                };
            }
            catch (Exception ex)
            {
                // Log the exception for debugging purposes
                //_logger?.LogError(ex, "Ocurrió un error al enviar el correo electrónico de consulta.");

                return new ServiceResult
                {
                    Success = false,
                    Message = $"Ocurrió un error al procesar la consulta. Por favor, intenta nuevamente más tarde."
                };
            }
        }
    }
}
