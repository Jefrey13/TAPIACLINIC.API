using Application.Commands.MedicalRecords;
using FluentValidation;

namespace Application.Validators
{
    public class UpdateMedicalRecordCommandValidator : AbstractValidator<UpdateMedicalRecordCommand>
    {
        public UpdateMedicalRecordCommandValidator()
        {
            // Validate that Id is required and greater than 0
            RuleFor(x => x.Id)
                .GreaterThan(0)
                .WithMessage("MedicalRecordId is required and must be greater than 0.");

            // Validate that PatientId is required and greater than 0
            RuleFor(x => x.MedicalRecordDto.PatientId)
                .GreaterThan(0)
                .WithMessage("PatientId is required and must be greater than 0.");

            // Validate that StaffId is required and greater than 0
            RuleFor(x => x.MedicalRecordDto.StaffId)
                .GreaterThan(0)
                .WithMessage("StaffId is required and must be greater than 0.");

            // Validate that OpeningDate is required
            RuleFor(x => x.MedicalRecordDto.OpeningDate)
                .NotEmpty()
                .WithMessage("OpeningDate is required.");

            // Validate that StateId is required and greater than 0
            RuleFor(x => x.MedicalRecordDto.StateId)
                .GreaterThan(0)
                .WithMessage("StateId is required and must be greater than 0.");

            // Optional validations for string fields with maximum length
            RuleFor(x => x.MedicalRecordDto.Allergies)
                .MaximumLength(255)
                .WithMessage("Allergies must not exceed 255 characters.");

            RuleFor(x => x.MedicalRecordDto.PastIllnesses)
                .MaximumLength(255)
                .WithMessage("PastIllnesses must not exceed 255 characters.");

            RuleFor(x => x.MedicalRecordDto.PastSurgeries)
                .MaximumLength(255)
                .WithMessage("PastSurgeries must not exceed 255 characters.");

            RuleFor(x => x.MedicalRecordDto.FamilyHistory)
                .MaximumLength(255)
                .WithMessage("FamilyHistory must not exceed 255 characters.");
        }
    }
}