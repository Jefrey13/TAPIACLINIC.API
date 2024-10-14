using Application.Commands.Surgeries;
using FluentValidation;

namespace Application.Validators
{
    /// <summary>
    /// Validator for CreateSurgeryCommand.
    /// </summary>
    public class CreateSurgeryCommandValidator : AbstractValidator<CreateSurgeryCommand>
    {
        public CreateSurgeryCommandValidator()
        {
            RuleFor(x => x.SurgeryDto.Name)
                .NotEmpty().WithMessage("Surgery name is required.")
                .MaximumLength(100).WithMessage("Surgery name must not exceed 100 characters.");

            RuleFor(x => x.SurgeryDto.Description)
                .MaximumLength(255).WithMessage("Surgery description must not exceed 255 characters.");

            RuleFor(x => x.SurgeryDto.StateId)
                .GreaterThan(0).WithMessage("State ID is required and must be greater than 0.");
        }
    }
}