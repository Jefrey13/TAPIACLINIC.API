using Application.Commands.Users;
using FluentValidation;

namespace Application.Validators
{
    /// <summary>
    /// Validator for the CreateUserCommand.
    /// Ensures that the user data is valid before proceeding with the creation.
    /// </summary>
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            // First Name validation
            RuleFor(x => x.UserDto.FirstName)
                .NotEmpty().WithMessage("First name is required.")
                .MaximumLength(100).WithMessage("First name must not exceed 100 characters.");

            // Last Name validation
            RuleFor(x => x.UserDto.LastName)
                .NotEmpty().WithMessage("Last name is required.")
                .MaximumLength(100).WithMessage("Last name must not exceed 100 characters.");

            // Email validation
            RuleFor(x => x.UserDto.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("A valid email is required.");

            // Last Name validation
            RuleFor(x => x.UserDto.Address)
                .NotEmpty().WithMessage("Last address is required.");

            // Password validation
            RuleFor(x => x.UserDto.Password)
                .NotEmpty().WithMessage("Password is required.")
                .MinimumLength(6).WithMessage("Password must be at least 6 characters long.");

            // Phone Address validation
            RuleFor(x => x.UserDto.PhoneAddress)
                .NotEmpty().WithMessage("Phone number is required.")
                .MaximumLength(15).WithMessage("Phone number must not exceed 15 characters.");

            // Gender validation
            RuleFor(x => x.UserDto.Gender)
                .NotEmpty().WithMessage("Gender is required.")
                .Must(gender => gender == "Male" || gender == "Female").WithMessage("Gender must be either 'Male' or 'Female'.");

            // Birth Date validation
            RuleFor(x => x.UserDto.BirthDate)
                .NotEmpty().WithMessage("Birth date is required.")
                .LessThanOrEqualTo(DateTime.Now.AddYears(-18)).WithMessage("User must be at least 18 years old.");

            // ID Card validation
            RuleFor(x => x.UserDto.IdCard)
                .NotEmpty().WithMessage("ID card is required.")
                .MaximumLength(20).WithMessage("ID card must not exceed 20 characters.");

            // StateId validation
            RuleFor(x => x.UserDto.StateId)
                .GreaterThan(0).WithMessage("A valid StateId is required.");

            // RoleId validation (optional, since it can be null)
            RuleFor(x => x.UserDto.RoleId)
                .GreaterThan(0).WithMessage("A valid StateId is required.");
        }
    }
}