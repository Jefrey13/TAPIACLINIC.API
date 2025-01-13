using Application.Commands.Appointments;
using FluentValidation;

namespace Application.Validators
{
    /// <summary>
    /// Validator for UpdateAppointmentCommand.
    /// </summary>
    public class UpdateAppointmentCommandValidator : AbstractValidator<UpdateAppointmentCommand>
    {
        public UpdateAppointmentCommandValidator()
        {

            RuleFor(x => x.AppointmentDto.PatientId)
                .GreaterThan(0).WithMessage("Patient ID is required.");

            RuleFor(x => x.AppointmentDto.SpecialtyId)
                .GreaterThan(0).WithMessage("Specialty ID is required.");

            RuleFor(x => x.AppointmentDto.ScheduleId)
                .GreaterThan(0).WithMessage("Schedule ID is required.");

            RuleFor(x => x.AppointmentDto.StateId)
                .GreaterThan(0).WithMessage("State ID is required.");
        }
    }
}