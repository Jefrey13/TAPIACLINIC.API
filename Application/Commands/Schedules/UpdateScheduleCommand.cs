using Application.Models.ReponseDtos;
using Application.Models.RequestDtos;
using MediatR;

namespace Application.Commands.Schedules
{
    /// <summary>
    /// Command to update an existing schedule.
    /// </summary>
    public class UpdateScheduleCommand : IRequest<Unit>
    {
        public int Id { get; set; }
        public ScheduleResquestDto ScheduleDto { get; set; }

        public UpdateScheduleCommand(int id, ScheduleResquestDto scheduleDto)
        {
            Id = id;
            ScheduleDto = scheduleDto;
        }
    }
}