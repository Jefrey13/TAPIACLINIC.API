using Application.Commands.MedicalRecords;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validators
{
    public class DeleteMedicalRecordCommandValidator : AbstractValidator<DeleteMedicalRecordCommand>
    {
        public DeleteMedicalRecordCommandValidator()
        {
            // Validate that Id is required and greater than 0
            RuleFor(x => x.Id)
                .GreaterThan(0)
                .WithMessage("MedicalRecordId is required and must be greater than 0.");
        }
    }
}