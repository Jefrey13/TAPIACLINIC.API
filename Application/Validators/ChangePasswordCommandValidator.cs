using Application.Commands.Auth;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validators
{
    public class ChangePasswordCommandValidator : AbstractValidator<ChangePasswordCommand>
    {
        public ChangePasswordCommandValidator()
        {
            RuleFor(x => x.RequestDto.UserName)
                .NotEmpty().WithMessage("Username is required.");

            RuleFor(x => x.RequestDto.CurrentPassword)
                .NotEmpty().WithMessage("Current password is required.");

            RuleFor(x => x.RequestDto.NewPassword)
                .NotEmpty().WithMessage("New password is required.")
                .MinimumLength(6).WithMessage("New password must be at least 6 characters long.");
        }
    }
}
