using Application.Commands.Schedules;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validators
{
    public class DeleteScheduleCommandValidator : AbstractValidator<DeleteScheduleCommand>
    {
        public DeleteScheduleCommandValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0)
                .WithMessage("Schedule ID is required.");
        }
    }
}
