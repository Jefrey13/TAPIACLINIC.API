using Application.Commands.Schedules;
using FluentValidation;
using System;

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
            // SpecialtyId must be greater than 0
            RuleFor(x => x.ScheduleDto.SpecialtyId)
                .GreaterThan(0)
                .WithMessage("SpecialtyId is required.");

            // DayOfWeek should not be empty
            RuleFor(x => x.ScheduleDto.DayOfWeek)
                .NotEmpty()
                .WithMessage("Day of the week is required.");

            // StartTime should not be empty and must be in "HH:mm:ss" format
            RuleFor(x => x.ScheduleDto.StartTime)
                .NotEmpty()
                .WithMessage("Start time is required.")
                .Must(BeValidTimeFormat).WithMessage("Start time must be in the format HH:mm:ss.");

            // EndTime should not be empty and must be in "HH:mm:ss" format
            RuleFor(x => x.ScheduleDto.EndTime)
                .NotEmpty()
                .WithMessage("End time is required.")
                .Must(BeValidTimeFormat).WithMessage("End time must be in the format HH:mm:ss.");

            // EndTime must be greater than StartTime
            RuleFor(x => x)
                .Must(x => BeLaterThan(x.ScheduleDto.StartTime, x.ScheduleDto.EndTime))
                .WithMessage("End time must be after the start time.");
        }

        // Validate that the string is in a valid TimeSpan format ("HH:mm:ss")
        private bool BeValidTimeFormat(string time)
        {
            return TimeSpan.TryParseExact(time, "hh\\:mm\\:ss", null, out _);
        }

        // Validate that EndTime is after StartTime
        private bool BeLaterThan(string startTime, string endTime)
        {
            if (TimeSpan.TryParse(startTime, out TimeSpan start) && TimeSpan.TryParse(endTime, out TimeSpan end))
            {
                return end > start;
            }
            return false;
        }
    }
}