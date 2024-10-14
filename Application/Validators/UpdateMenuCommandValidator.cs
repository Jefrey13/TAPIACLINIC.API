using Application.Commands.Menus;
using FluentValidation;

namespace Application.Validators
{
    /// <summary>
    /// Validator for UpdateMenuCommand.
    /// </summary>
    public class UpdateMenuCommandValidator : AbstractValidator<UpdateMenuCommand>
    {
        public UpdateMenuCommandValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("El ID del menú debe ser mayor que 0.");

            RuleFor(x => x.MenuDto.Name)
                .NotEmpty().WithMessage("El nombre del menú es obligatorio.")
                .MaximumLength(100).WithMessage("El nombre del menú no debe exceder los 100 caracteres.");

            RuleFor(x => x.MenuDto.Url)
                .MaximumLength(255).WithMessage("La URL del menú no debe exceder los 255 caracteres.")
                .When(x => !string.IsNullOrEmpty(x.MenuDto.Url));

            RuleFor(x => x.MenuDto.Icon)
                .MaximumLength(100).WithMessage("El icono del menú no debe exceder los 100 caracteres.")
                .When(x => !string.IsNullOrEmpty(x.MenuDto.Icon));

            RuleFor(x => x.MenuDto.Order)
                .GreaterThan(0).WithMessage("El orden del menú debe ser mayor que 0.");
        }
    }
}