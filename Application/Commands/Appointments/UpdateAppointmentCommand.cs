using Application.Models;
using MediatR;

namespace Application.Commands.Appointments
{
    /// <summary>
    /// Command to update an existing appointment.
    /// </summary>
    public class UpdateAppointmentCommand : IRequest<Unit>
    {
        public int Id { get; set; }
        public AppointmentDto AppointmentDto { get; set; }

        public UpdateAppointmentCommand(int id, AppointmentDto appointmentDto)
        {
            Id = id;
            AppointmentDto = appointmentDto;
        }
    }
}