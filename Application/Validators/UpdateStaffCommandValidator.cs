using Application.Commands.Staffs;
using FluentValidation;

namespace Application.Commands.Staffs
{
    /// <summary>
    /// Validator for the UpdateStaffCommand.
    /// Ensures that the required fields for staff updates are valid.
    /// </summary>
    public class UpdateStaffCommandValidator : AbstractValidator<UpdateStaffCommand>
    {
        public UpdateStaffCommandValidator()
        {
            // Validate that the StaffId is greater than 0
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("Staff ID must be greater than 0.");

            // Validate that the User information is provided and correct if present
            RuleFor(x => x.StaffDto.User).NotNull().WithMessage("User information is required.");

            // Validate that the FirstName is not empty
            RuleFor(x => x.StaffDto.User.FirstName)
                .NotEmpty().WithMessage("First Name is required.")
                .MaximumLength(100).WithMessage("First Name cannot exceed 100 characters.");

            // Validate that the LastName is not empty
            RuleFor(x => x.StaffDto.User.LastName)
                .NotEmpty().WithMessage("Last Name is required.")
                .MaximumLength(100).WithMessage("Last Name cannot exceed 100 characters.");

            //// Validate Email
            //RuleFor(x => x.StaffDto.User.Email)
            //    .NotEmpty().WithMessage("Email is required.")
            //    .EmailAddress().WithMessage("Invalid email format.");

            // Validate that SpecialtyId is optional but must be valid if provided
            RuleFor(x => x.StaffDto.SpecialtyId)
                .GreaterThan(0).When(x => x.StaffDto.SpecialtyId.HasValue)
                .WithMessage("SpecialtyId must be greater than 0 if provided.");
        }
    }
}