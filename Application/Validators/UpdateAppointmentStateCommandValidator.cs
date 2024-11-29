using Application.Commands.Appointments;
using FluentValidation;

namespace Application.Validators
{
    public class UpdateAppointmentStateCommandValidator : AbstractValidator<UpdateAppointmentStateCommand>
    {
        public UpdateAppointmentStateCommandValidator()
        {
            RuleFor(x => x.AppointmentId)
                .GreaterThan(0).WithMessage("AppointmentId must be a positive integer.");

            RuleFor(x => x.NewStateName)
                .NotEmpty().WithMessage("NewStateName is required.")
                .MaximumLength(50).WithMessage("NewStateName must not exceed 50 characters.");
        }
    }
}