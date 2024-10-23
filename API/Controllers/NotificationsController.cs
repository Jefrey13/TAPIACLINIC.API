using Application.Models.RequestDtos;
using Application.Services;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
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

        public EmailController(IEmailSender emailSender, IValidator<EmailRequestDto> validator)
        {
            _emailSender = emailSender;
            _validator = validator;
        }

        /// <summary>
        /// Endpoint to send an email based on the provided request data.
        /// </summary>
        /// <param name="emailRequestDto">The email request DTO.</param>
        /// <returns>HTTP 200 if the email is sent successfully, otherwise validation errors.</returns>
        [HttpPost]
        [Route("send")]
        public async Task<IActionResult> SendEmail([FromBody] EmailRequestDto emailRequestDto)
        {
            var validationResult = await _validator.ValidateAsync(emailRequestDto);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            // Use the individual properties from EmailRequestDto to send the email
            await _emailSender.SendEmailAsync(emailRequestDto.To, emailRequestDto.Subject, emailRequestDto.Body);
            return Ok("Email sent successfully.");
        }
    }
}