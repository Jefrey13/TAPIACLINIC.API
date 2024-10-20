using FluentValidation;
using Application.Commands.States;

namespace Application.Validators
{
    public class UpdateStateCommandValidator : AbstractValidator<UpdateStateCommand>
    {
        public UpdateStateCommandValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("State ID must be greater than 0.");

            RuleFor(x => x.StateDto.StateName)
                .NotEmpty().WithMessage("State name is required.")
                .MaximumLength(50).WithMessage("State name must not exceed 50 characters.");

            RuleFor(x => x.StateDto.Description)
                .MaximumLength(255).WithMessage("Description must not exceed 255 characters.");
        }
    }
}