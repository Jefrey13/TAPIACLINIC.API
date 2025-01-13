using Application.Commands.Appointments;
using FluentValidation;

namespace Application.Validators
{
    /// <summary>
    /// Validator for CreateAppointmentCommand.
    /// </summary>
    public class CreateAppointmentCommandValidator : AbstractValidator<CreateAppointmentCommand>
    {
        public CreateAppointmentCommandValidator()
        {
            RuleFor(x => x.AppointmentDto.SpecialtyId)
                .GreaterThan(0).WithMessage("Specialty ID is required.");

            RuleFor(x => x.AppointmentDto.ScheduleId)
                .GreaterThan(0).WithMessage("Schedule ID is required.");

            RuleFor(x => x.AppointmentDto.StateId)
                .GreaterThan(0).WithMessage("State ID is required.");
        }
    }
}