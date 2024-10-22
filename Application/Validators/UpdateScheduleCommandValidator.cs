using Application.Commands.Schedules;
using FluentValidation;

namespace Application.Validators
{
    /// <summary>
    /// Validator for UpdateScheduleCommand.
    /// Validates the properties of the ScheduleDto within the command.
    /// </summary>
    public class UpdateScheduleCommandValidator : AbstractValidator<UpdateScheduleCommand>
    {
        public UpdateScheduleCommandValidator()
        {

            RuleFor(x => x.ScheduleDto.SpecialtyId)
                .GreaterThan(0)
                .WithMessage("SpecialtyId is required.");

            RuleFor(x => x.ScheduleDto.DayOfWeek)
                .NotEmpty()
                .WithMessage("Day of the week is required.");

            RuleFor(x => x.ScheduleDto.StartTime)
                .NotEmpty()
                .WithMessage("Start time is required.");

            RuleFor(x => x.ScheduleDto.EndTime)
                .NotEmpty()
                .WithMessage("End time is required.");

            RuleFor(x => x.ScheduleDto.EndTime)
                .GreaterThan(x => x.ScheduleDto.StartTime)
                .WithMessage("End time must be after the start time.");
        }
    }
}