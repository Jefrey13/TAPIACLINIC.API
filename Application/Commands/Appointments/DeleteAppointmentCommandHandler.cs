using Application.Commands.Appointments;
using Application.Exceptions;
using Domain.Entities;
using Domain.Repositories;
using MediatR;

namespace Application.Handlers.Appointments
{
    /// <summary>
    /// Handler for deleting an appointment.
    /// </summary>
    public class DeleteAppointmentCommandHandler : IRequestHandler<DeleteAppointmentCommand, Unit>
    {
        private readonly IAppointmentRepository _appointmentRepository;

        public DeleteAppointmentCommandHandler(IAppointmentRepository appointmentRepository)
        {
            _appointmentRepository = appointmentRepository;
        }

        public async Task<Unit> Handle(DeleteAppointmentCommand request, CancellationToken cancellationToken)
        {
            var appointment = await _appointmentRepository.GetByIdAsync(request.Id);
            if (appointment == null)
            {
                throw new NotFoundException(nameof(Appointment), request.Id);
            }

            await _appointmentRepository.DeleteAsync(appointment);
            return Unit.Value;
        }
    }
}