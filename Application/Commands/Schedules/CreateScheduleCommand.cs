using Application.Models.RequestDtos;
using MediatR;

namespace Application.Commands.Schedules
{
    /// <summary>
    /// Command to create a new Schedule.
    /// </summary>
    public class CreateScheduleCommand : IRequest<int>
    {
        public ScheduleResquestDto ScheduleDto { get; }

        public CreateScheduleCommand(ScheduleResquestDto scheduleDto)
        {
            ScheduleDto = scheduleDto;
        }
    }
}