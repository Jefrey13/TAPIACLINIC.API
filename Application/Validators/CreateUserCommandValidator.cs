using Application.Commands.Users;
using FluentValidation;

namespace Application.Validators
{
    /// <summary>
    /// Validator for CreateUserCommand.
    /// Validates the properties of the UserDto within the command.
    /// </summary>
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            RuleFor(x => x.UserDto.FirstName)
                .NotEmpty().WithMessage("First name is required.")
                .MaximumLength(100).WithMessage("First name must not exceed 100 characters.");

            RuleFor(x => x.UserDto.LastName)
                .NotEmpty().WithMessage("Last name is required.")
                .MaximumLength(100).WithMessage("Last name must not exceed 100 characters.");

            RuleFor(x => x.UserDto.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid email format.");

            RuleFor(x => x.UserDto.Phone)
                .NotEmpty().WithMessage("Phone number is required.")
                .Matches(@"^\d{8}$").WithMessage("Phone number must be exactly 8 digits.");

            RuleFor(x => x.UserDto.Address)
                .MaximumLength(255).WithMessage("Address must not exceed 255 characters.");

            RuleFor(x => x.UserDto.Gender)
                .NotEmpty().WithMessage("Gender is required.")
                .Must(g => g == "M" || g == "F").WithMessage("Gender must be either 'M' or 'F'.");

            RuleFor(x => x.UserDto.BirthDate)
                .LessThan(DateTime.Now).WithMessage("Birth date must be in the past.");

            RuleFor(x => x.UserDto.IdCard)
                .NotEmpty().WithMessage("ID card is required.")
                .MaximumLength(20).WithMessage("ID card must not exceed 20 characters.");

            RuleFor(x => x.UserDto.RoleId)
                .GreaterThan(0).When(x => x.UserDto.RoleId.HasValue)
                .WithMessage("If provided, RoleId must be greater than 0.");

            RuleFor(x => x.UserDto.StateId)
                .GreaterThan(0).WithMessage("StateId must be greater than 0.");
        }
    }
}