using Application.Commands.Staffs;
using FluentValidation;

namespace Application.Validators
{
    public class CreateStaffCommandValidator : AbstractValidator<CreateStaffCommand>
    {
        public CreateStaffCommandValidator()
        {
            RuleFor(x => x.StaffDto.User.FirstName)
                .NotEmpty().WithMessage("First name is required.")
                .MaximumLength(100).WithMessage("First name must not exceed 100 characters.");

            RuleFor(x => x.StaffDto.User.LastName)
                .NotEmpty().WithMessage("Last name is required.")
                .MaximumLength(100).WithMessage("Last name must not exceed 100 characters.");

            RuleFor(x => x.StaffDto.MinsaCode)
                .MaximumLength(50).WithMessage("MINSA code must not exceed 50 characters.");
        }
    }
}