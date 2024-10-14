using Application.Commands.MedicalRecords;
using FluentValidation;

namespace Application.Validators
{
    public class UpdateMedicalRecordCommandValidator : AbstractValidator<UpdateMedicalRecordCommand>
    {
        public UpdateMedicalRecordCommandValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("Record ID must be greater than 0.");

            RuleFor(x => x.MedicalRecordDto.PatientId)
                .GreaterThan(0).WithMessage("Patient ID is required.");

            RuleFor(x => x.MedicalRecordDto.StaffId)
                .GreaterThan(0).WithMessage("Staff ID is required.");

            RuleFor(x => x.MedicalRecordDto.OpeningDate)
                .NotEmpty().WithMessage("Opening date is required.");
        }
    }
}