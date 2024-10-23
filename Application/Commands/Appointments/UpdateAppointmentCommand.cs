using Application.Models.RequestDtos;
using MediatR;

namespace Application.Commands.Appointments
{
    /// <summary>
    /// Command to update an existing appointment.
    /// </summary>
    public class UpdateAppointmentCommand : IRequest<Unit>
    {
        public int Id { get; set; }
        public AppointmentRequestDto AppointmentDto { get; set; }

        public UpdateAppointmentCommand(int id, AppointmentRequestDto appointmentDto)
        {
            Id = id;
            AppointmentDto = appointmentDto;
        }
    }
}