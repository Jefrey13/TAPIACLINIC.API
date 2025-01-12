using Application.Commands.Prescriptions;
using FluentValidation;

namespace Application.Validators
{
    /// <summary>
    /// Validator for CreatePrescriptionCommand.
    /// Ensures that the command has valid data before processing.
    /// </summary>
    public class CreatePrescriptionCommandValidator : AbstractValidator<CreatePrescriptionCommand>
    {
        public CreatePrescriptionCommandValidator()
        {
            // Validate that PatientId is required and greater than 0
            RuleFor(x => x.PrescriptionDto.PatientId)
                .GreaterThan(0)
                .WithMessage("PatientId is required and must be greater than 0.");

            // Validate that PrescriptionDate is required
            RuleFor(x => x.PrescriptionDto.PrescriptionDate)
                .NotEmpty()
                .WithMessage("PrescriptionDate is required.");

            // Validate that Diagnosis is required and has a maximum length
            RuleFor(x => x.PrescriptionDto.Diagnosis)
                .NotEmpty()
                .WithMessage("Diagnosis is required.")
                .MaximumLength(255)
                .WithMessage("Diagnosis must not exceed 255 characters.");

            // Validate that Treatment is required and has a maximum length
            RuleFor(x => x.PrescriptionDto.Treatment)
                .NotEmpty()
                .WithMessage("Treatment is required.")
                .MaximumLength(500)
                .WithMessage("Treatment must not exceed 500 characters.");

            // Optional validation for Notes with maximum length
            RuleFor(x => x.PrescriptionDto.Notes)
                .MaximumLength(500)
                .WithMessage("Notes must not exceed 500 characters.");

            // Optional validation for Observations with maximum length
            RuleFor(x => x.PrescriptionDto.Observations)
                .MaximumLength(500)
                .WithMessage("Observations must not exceed 500 characters.");
        }
    }
}