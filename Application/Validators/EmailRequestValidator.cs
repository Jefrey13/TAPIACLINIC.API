using Application.Models.RequestDtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validators
{
    /// <summary>
    /// Validator for EmailRequestDto to ensure that the email request has valid data.
    /// </summary>
    public class EmailRequestValidator : AbstractValidator<EmailRequestDto>
    {
        public EmailRequestValidator()
        {
            // Ensure 'To' is not empty and is a valid email address
            RuleFor(x => x.To)
                .NotEmpty().WithMessage("Recipient email address is required.")
                .EmailAddress().WithMessage("Invalid email address format.");

            // Ensure 'Subject' is not empty and does not exceed 255 characters
            RuleFor(x => x.Subject)
                .NotEmpty().WithMessage("Email subject is required.")
                .MaximumLength(255).WithMessage("Subject cannot exceed 255 characters.");

            // Ensure 'Body' is not empty
            RuleFor(x => x.Body)
                .NotEmpty().WithMessage("Email body is required.");
        }
    }
}