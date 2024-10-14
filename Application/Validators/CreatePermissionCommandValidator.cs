using Application.Commands.Permissions;
using FluentValidation;

namespace Application.Validators
{
    /// <summary>
    /// Validator for CreatePermissionCommand.
    /// Validates the properties of the PermissionDto within the command.
    /// </summary>
    public class CreatePermissionCommandValidator : AbstractValidator<CreatePermissionCommand>
    {
        public CreatePermissionCommandValidator()
        {
            RuleFor(x => x.PermissionDto.Name)
                .NotEmpty().WithMessage("Permission name is required.")
                .MaximumLength(100).WithMessage("Permission name must not exceed 100 characters.");

            RuleFor(x => x.PermissionDto.Description)
                .MaximumLength(255).WithMessage("Description must not exceed 255 characters.");
        }
    }
}