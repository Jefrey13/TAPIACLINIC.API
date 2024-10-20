using FluentValidation;
using Application.Commands.States;

namespace Application.Validators
{
    public class CreateStateCommandValidator : AbstractValidator<CreateStateCommand>
    {
        public CreateStateCommandValidator()
        {
            RuleFor(x => x.StateDto.StateName)
                .NotEmpty().WithMessage("State name is required.")
                .MaximumLength(50).WithMessage("State name must not exceed 50 characters.");

            RuleFor(x => x.StateDto.Description)
                .MaximumLength(255).WithMessage("Description must not exceed 255 characters.");
        }
    }
}