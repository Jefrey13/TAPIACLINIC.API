using Application.Commands.Users;
using FluentValidation;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Numerics;

namespace Application.Validators
{
    /// <summary>
    /// Validator for the CreateUserCommand.
    /// Ensures that the user data is valid before proceeding with the creation.
    /// </summary>
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            // First Name validation
            RuleFor(x => x.UserDto.FirstName)
                .NotEmpty().WithMessage("First name is required.")
                .MaximumLength(100).WithMessage("First name must not exceed 100 characters.");

            // Last Name validation
            RuleFor(x => x.UserDto.LastName)
                .NotEmpty().WithMessage("Last name is required.")
                .MaximumLength(100).WithMessage("Last name must not exceed 100 characters.");

            // Email validation
            RuleFor(x => x.UserDto.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("A valid email is required.");

            // Last Name validation
            RuleFor(x => x.UserDto.Address)
                .NotEmpty().WithMessage("Last address is required.");

            // Password validation
            RuleFor(x => x.UserDto.Password)
                .NotEmpty().WithMessage("Password is required.")
                .MinimumLength(6).WithMessage("Password must be at least 6 characters long.");

            RuleFor(x => x.UserDto.PhoneAddress)
    .NotEmpty().WithMessage("El número de teléfono es obligatorio.")
    .Matches(@"^\+505(5|7|8)\d{6}$").WithMessage("El número de teléfono debe comenzar con +505 y tener 8 caracteres, empezando con 5, 7 o 8.");


            // Gender validation
            RuleFor(x => x.UserDto.Gender)
                .NotEmpty().WithMessage("Gender is required.")
                .Must(gender => gender == "Male" || gender == "Female").WithMessage("Gender must be either 'Male' or 'Female'.");

            RuleFor(x => x.UserDto.BirthDate)
    .NotEmpty().WithMessage("La fecha de nacimiento es obligatoria.")
    .Must(date => date <= DateTime.Today).WithMessage("La fecha de nacimiento no puede ser mayor a la fecha actual.");


            RuleFor(x => x.UserDto.IdCard)
    .NotEmpty().WithMessage("El número de cédula es obligatorio.")
    .Matches(@"^\d{3}-\d{6}-\d{4}[A-Z]$").WithMessage("El formato de la cédula debe ser 000-000000-0000X, donde X es una letra mayúscula.");


            // StateId validation
            //RuleFor(x => x.UserDto.StateId)
            //    .GreaterThan(0).WithMessage("A valid StateId is required.");

            // RoleId validation (optional, since it can be null)
            //RuleFor(x => x.UserDto.RoleId)
            //    .GreaterThan(0).WithMessage("A valid StateId is required.");
        }
    }
}