using Application.Commands.Roles;
using FluentValidation;

namespace Application.Validators
{
    /// <summary>
    /// Validator for CreateRoleCommand.
    /// Validates the properties of the RoleDto within the command.
    /// </summary>
    public class CreateRoleCommandValidator : AbstractValidator<CreateRoleCommand>
    {
        public CreateRoleCommandValidator()
        {
            RuleFor(x => x.RoleDto.Name)
                .NotEmpty().WithMessage("Role name is required.")
                .MaximumLength(50).WithMessage("Role name must not exceed 50 characters.");

            RuleFor(x => x.RoleDto.PermissionIds)
                .NotEmpty().WithMessage("At least one permission must be assigned to the role.");

            RuleFor(x => x.RoleDto.MenuIds)
                .NotEmpty().WithMessage("At least one menu must be assigned to the role.");
        }
    }
}