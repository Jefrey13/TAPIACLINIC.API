using Application.Commands.MedicalRecords;
using FluentValidation;

namespace Application.Validators
{
    public class CreateMedicalRecordCommandValidator : AbstractValidator<CreateMedicalRecordCommand>
    {
        public CreateMedicalRecordCommandValidator()
        {
            // Validate that PatientId is required and greater than 0
            RuleFor(x => x.MedicalRecordDto.PatientId)
                .GreaterThan(0)
                .WithMessage("PatientId is required and must be greater than 0.");

            // Validate that OpeningDate is required
            RuleFor(x => x.MedicalRecordDto.OpeningDate)
                .NotEmpty()
                .WithMessage("OpeningDate is required.");

            // Validate that StateId is required and greater than 0
            RuleFor(x => x.MedicalRecordDto.StateId)
                .GreaterThan(0)
                .WithMessage("StateId is required and must be greater than 0.");
        }
    }
}