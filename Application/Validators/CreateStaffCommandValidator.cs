using Application.Commands.Staffs;
using FluentValidation;

namespace Application.Commands.Staffs
{
    /// <summary>
    /// Validator for the CreateStaffCommand.
    /// Ensures that the required fields for staff creation are provided and valid.
    /// </summary>
    public class CreateStaffCommandValidator : AbstractValidator<CreateStaffCommand>
    {
        public CreateStaffCommandValidator()
        {
            // Validate that the User is provided
            RuleFor(x => x.StaffDto.User).NotNull().WithMessage("User information is required.");

            // Validate that the UserName is not empty
            RuleFor(x => x.StaffDto.User.UserName)
                .NotEmpty().WithMessage("Username is required.")
                .MaximumLength(100).WithMessage("Username cannot exceed 100 characters.");

            // Validate that the FirstName is not empty
            RuleFor(x => x.StaffDto.User.FirstName)
                .NotEmpty().WithMessage("First Name is required.")
                .MaximumLength(100).WithMessage("First Name cannot exceed 100 characters.");

            // Validate that the LastName is not empty
            RuleFor(x => x.StaffDto.User.LastName)
                .NotEmpty().WithMessage("Last Name is required.")
                .MaximumLength(100).WithMessage("Last Name cannot exceed 100 characters.");

            // Validate Email
            RuleFor(x => x.StaffDto.User.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid email format.");

            // Validate that SpecialtyId is optional but must be valid if provided
            RuleFor(x => x.StaffDto.SpecialtyId)
                .GreaterThan(0).When(x => x.StaffDto.SpecialtyId.HasValue)
                .WithMessage("SpecialtyId must be greater than 0 if provided.");
        }
    }
}