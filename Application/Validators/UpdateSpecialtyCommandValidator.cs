using Application.Commands.Specialties;
using FluentValidation;

namespace Application.Validators
{
    /// <summary>
    /// Validator for UpdateSpecialtyCommand.
    /// </summary>
    public class UpdateSpecialtyCommandValidator : AbstractValidator<UpdateSpecialtyCommand>
    {
        public UpdateSpecialtyCommandValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("Specialty ID must be greater than 0.");

            RuleFor(x => x.SpecialtyDto.Name)
                .NotEmpty().WithMessage("Specialty name is required.")
                .MaximumLength(100).WithMessage("Specialty name must not exceed 100 characters.");

            RuleFor(x => x.SpecialtyDto.Description)
                .MaximumLength(255).WithMessage("Specialty description must not exceed 255 characters.");
        }
    }
}