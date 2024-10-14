using Application.Models;
using MediatR;

namespace Application.Commands.Appointments
{
    /// <summary>
    /// Command to create a new appointment.
    /// </summary>
    public class CreateAppointmentCommand : IRequest<int>
    {
        public AppointmentDto AppointmentDto { get; set; }

        public CreateAppointmentCommand(AppointmentDto appointmentDto)
        {
            AppointmentDto = appointmentDto;
        }
    }
}