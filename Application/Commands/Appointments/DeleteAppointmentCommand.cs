using MediatR;

namespace Application.Commands.Appointments
{
    /// <summary>
    /// Command to delete an appointment by ID.
    /// </summary>
    public class DeleteAppointmentCommand : IRequest<Unit>
    {
        public int Id { get; set; }

        public DeleteAppointmentCommand(int id)
        {
            Id = id;
        }
    }
}