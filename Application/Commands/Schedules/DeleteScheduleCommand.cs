using MediatR;

namespace Application.Commands.Schedules
{
    /// <summary>
    /// Command to delete a schedule by ID.
    /// </summary>
    public class DeleteScheduleCommand : IRequest<Unit>
    {
        public int Id { get; set; }

        public DeleteScheduleCommand(int id)
        {
            Id = id;
        }
    }
}