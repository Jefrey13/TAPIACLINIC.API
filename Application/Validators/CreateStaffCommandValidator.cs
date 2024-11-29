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

            // Validate that SpecialtyId is optional but must be valid if provided
            RuleFor(x => x.StaffDto.SpecialtyId)
                .GreaterThan(0).When(x => x.StaffDto.SpecialtyId.HasValue)
                .WithMessage("SpecialtyId must be greater than 0 if provided.");
        }
    }
}