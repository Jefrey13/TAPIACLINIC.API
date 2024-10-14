using Application.Commands.Roles;
using FluentValidation;

namespace Application.Validators
{
    /// <summary>
    /// Validator for UpdateRoleCommand.
    /// Validates the properties of the RoleDto within the command.
    /// </summary>
    public class UpdateRoleCommandValidator : AbstractValidator<UpdateRoleCommand>
    {
        public UpdateRoleCommandValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("Role ID must be greater than 0.");

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