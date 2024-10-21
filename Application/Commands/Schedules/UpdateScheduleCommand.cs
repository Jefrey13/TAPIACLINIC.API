using Application.Models.ReponseDtos;
using MediatR;

namespace Application.Commands.Schedules
{
    /// <summary>
    /// Command to update an existing schedule.
    /// </summary>
    public class UpdateScheduleCommand : IRequest<Unit>
    {
        public int Id { get; set; }
        public ScheduleDto ScheduleDto { get; set; }

        public UpdateScheduleCommand(int id, ScheduleDto scheduleDto)
        {
            Id = id;
            ScheduleDto = scheduleDto;
        }
    }
}