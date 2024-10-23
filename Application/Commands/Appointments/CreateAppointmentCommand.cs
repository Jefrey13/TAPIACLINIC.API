using Application.Models.RequestDtos;
using MediatR;

namespace Application.Commands.Appointments
{
    /// <summary>
    /// Command to create a new appointment.
    /// </summary>
    public class CreateAppointmentCommand : IRequest<int>
    {
        public AppointmentRequestDto AppointmentDto { get; set; }

        public CreateAppointmentCommand(AppointmentRequestDto appointmentDto)
        {
            AppointmentDto = appointmentDto;
        }
    }
}