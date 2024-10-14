using Application.Commands.Permissions;
using FluentValidation;

namespace Application.Validators
{
    /// <summary>
    /// Validator for UpdatePermissionCommand.
    /// Validates the properties of the PermissionDto within the command.
    /// </summary>
    public class UpdatePermissionCommandValidator : AbstractValidator<UpdatePermissionCommand>
    {
        public UpdatePermissionCommandValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("Permission ID must be greater than 0.");

            RuleFor(x => x.PermissionDto.Name)
                .NotEmpty().WithMessage("Permission name is required.")
                .MaximumLength(100).WithMessage("Permission name must not exceed 100 characters.");

            RuleFor(x => x.PermissionDto.Description)
                .MaximumLength(255).WithMessage("Description must not exceed 255 characters.");
        }
    }
}