using Application.Models.RequestDtos;
using Application.Services;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using API.Utils;
using System.Threading.Tasks;

namespace API.Controllers
{
    /// <summary>
    /// Controller to handle email-related actions.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly IEmailSender _emailSender;
        private readonly IValidator<EmailRequestDto> _validator;

        /// <summary>
        /// Constructor for the EmailController.
        /// </summary>
        /// <param name="emailSender">Service to handle email sending.</param>
        /// <param name="validator">Validator for the EmailRequestDto.</param>
        public EmailController(IEmailSender emailSender, IValidator<EmailRequestDto> validator)
        {
            _emailSender = emailSender;
            _validator = validator;
        }

        /// <summary>
        /// Endpoint to send an email based on the provided request data.
        /// </summary>
        /// <param name="emailRequestDto">The email request DTO.</param>
        /// <returns>HTTP 200 if the email is sent successfully, or validation errors in ApiResponse format.</returns>
        [HttpPost("send")]
        public async Task<ActionResult<ApiResponse<string>>> SendEmail([FromBody] EmailRequestDto emailRequestDto)
        {
            // Validate the request using FluentValidation
            var validationResult = await _validator.ValidateAsync(emailRequestDto);
            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors
                    .GroupBy(e => e.PropertyName)
                    .ToDictionary(
                        g => g.Key,
                        g => string.Join(", ", g.Select(e => e.ErrorMessage))
                    );

                var errorResponse = new ApiResponse<string>(false, "Validation failed", null, 400)
                {
                    Errors = errors
                };
                return BadRequest(errorResponse);
            }

            // Send the email using the email service
            await _emailSender.SendEmailAsync(emailRequestDto.To, emailRequestDto.Subject, emailRequestDto.Body);

            var response = new ApiResponse<string>(true, "Email sent successfully", null, 200);
            return Ok(response);
        }

    }
}