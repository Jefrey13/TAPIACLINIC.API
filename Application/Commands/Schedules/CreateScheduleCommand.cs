using Application.Models;
using MediatR;

namespace Application.Commands.Schedules
{
    /// <summary>
    /// Command to create a new schedule.
    /// This command contains the data required to create a schedule via ScheduleDto.
    /// </summary>
    public class CreateScheduleCommand : IRequest<int>
    {
        public ScheduleDto ScheduleDto { get; set; }

        public CreateScheduleCommand(ScheduleDto scheduleDto)
        {
            ScheduleDto = scheduleDto;
        }
    }
}