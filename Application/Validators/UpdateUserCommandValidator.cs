//using Application.Commands.Users;
//using FluentValidation;

//namespace Application.Validators
//{
//    /// <summary>
//    /// Validator for the UpdateUserCommand.
//    /// Ensures that the user data is valid before proceeding with the update.
//    /// </summary>
//    public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
//    {
//        public UpdateUserCommandValidator()
//        {
//            RuleFor(x => x.UserDto.FirstName)
//                .NotEmpty().WithMessage("First name is required.")
//                .MaximumLength(100).WithMessage("First name must not exceed 100 characters.");

//            RuleFor(x => x.UserDto.LastName)
//                .NotEmpty().WithMessage("Last name is required.")
//                .MaximumLength(100).WithMessage("Last name must not exceed 100 characters.");

//            RuleFor(x => x.UserDto.Email)
//                .NotEmpty().WithMessage("Email is required.")
//                .EmailAddress().WithMessage("A valid email is required.");

//            // Validar campos opcionales solo si están presentes
//            RuleFor(x => x.UserDto.Address)
//                .MaximumLength(255).When(x => !string.IsNullOrEmpty(x.UserDto.Address));

//            RuleFor(x => x.UserDto.PhoneAddress)
//                .MaximumLength(15).When(x => !string.IsNullOrEmpty(x.UserDto.PhoneAddress));

//            //RuleFor(x => x.UserDto.BirthDate)
//            //    .LessThanOrEqualTo(DateTime.Now.AddYears(-18))
//            //    .When(x => x.UserDto.BirthDate.HasValue)
//            //    .WithMessage("User must be at least 18 years old.");

//            RuleFor(x => x.UserDto.Gender)
//                .Must(g => g == "Male" || g == "Female")
//                .When(x => !string.IsNullOrEmpty(x.UserDto.Gender))
//                .WithMessage("Gender must be either 'Male' or 'Female'.");

//            //RuleFor(x => x.UserDto.StateId)
//            //    .GreaterThan(0).When(x => x.UserDto.StateId.HasValue)
//            //    .WithMessage("A valid StateId is required.");
//        }
//    }
//}