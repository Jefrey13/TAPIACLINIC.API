using Application.Commands.Schedules;
using FluentValidation;

namespace Application.Validators
{
    /// <summary>
    /// Validator for CreateScheduleCommand.
    /// Validates the properties of the ScheduleDto within the command.
    /// </summary>
    public class CreateScheduleCommandValidator : AbstractValidator<CreateScheduleCommand>
    {
        public CreateScheduleCommandValidator()
        {
            RuleFor(x => x.ScheduleDto.StaffId)
                .GreaterThan(0).WithMessage("Staff ID must be greater than 0.");

            RuleFor(x => x.ScheduleDto.DayOfWeek)
                .NotEmpty().WithMessage("Day of the week is required.")
                .MaximumLength(10).WithMessage("Day of the week must not exceed 10 characters.");

            RuleFor(x => x.ScheduleDto.StartTime)
                .NotNull().WithMessage("Start time is required.");

            RuleFor(x => x.ScheduleDto.EndTime)
                .NotNull().WithMessage("End time is required.")
                .GreaterThan(x => x.ScheduleDto.StartTime).WithMessage("End time must be greater than start time.");
        }
    }
}