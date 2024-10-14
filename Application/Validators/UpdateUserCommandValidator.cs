using Application.Commands.Users;
using FluentValidation;

namespace Application.Validators
{
    /// <summary>
    /// Validator for UpdateUserCommand.
    /// Validates the properties of the UserDto within the command.
    /// </summary>
    public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
    {
        public UpdateUserCommandValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("User ID must be greater than 0.");

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
                .Matches(@"^\d{8}$").WithMessage("Phone must be exactly 8 digits.");

            RuleFor(x => x.UserDto.Gender)
                .IsInEnum().WithMessage("Gender must be either 'M' or 'F'.");

            RuleFor(x => x.UserDto.IdCard)
                .NotEmpty().WithMessage("ID Card is required.")
                .MaximumLength(20).WithMessage("ID Card must not exceed 20 characters.");
        }
    }
}