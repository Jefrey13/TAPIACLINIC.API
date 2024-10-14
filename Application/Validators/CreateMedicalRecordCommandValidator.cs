using Application.Commands.MedicalRecords;
using FluentValidation;

namespace Application.Validators
{
    public class CreateMedicalRecordCommandValidator : AbstractValidator<CreateMedicalRecordCommand>
    {
        public CreateMedicalRecordCommandValidator()
        {
            RuleFor(x => x.MedicalRecordDto.PatientId)
                .GreaterThan(0).WithMessage("Patient ID is required.");

            RuleFor(x => x.MedicalRecordDto.StaffId)
                .GreaterThan(0).WithMessage("Staff ID is required.");

            RuleFor(x => x.MedicalRecordDto.OpeningDate)
                .NotEmpty().WithMessage("Opening date is required.");

            RuleFor(x => x.MedicalRecordDto.Allergies)
                .MaximumLength(255).WithMessage("Allergies must not exceed 255 characters.");
        }
    }
}