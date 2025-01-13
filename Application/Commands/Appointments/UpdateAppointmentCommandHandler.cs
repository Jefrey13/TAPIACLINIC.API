using Application.Commands.Appointments;
using Application.Exceptions;
using AutoMapper;
using Domain.Entities;
using Domain.Repositories;
using MediatR;

namespace Application.Handlers.Appointments
{
    /// <summary>
    /// Handler for updating an existing appointment.
    /// </summary>
    public class UpdateAppointmentCommandHandler : IRequestHandler<UpdateAppointmentCommand, Unit>
    {
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IMapper _mapper;

        public UpdateAppointmentCommandHandler(IAppointmentRepository appointmentRepository, IMapper mapper)
        {
            _appointmentRepository = appointmentRepository;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateAppointmentCommand request, CancellationToken cancellationToken)
        {
            try
            {
                // Validar el ID de la cita
                if (request.Id <= 0)
                {
                    throw new ArgumentException("Invalid appointment ID");
                }

                // Validar si los datos del DTO son válidos
                if (request.AppointmentDto == null)
                {
                    throw new ArgumentException("Appointment data is required");
                }

                var appointment = await _appointmentRepository.GetByIdAsync(request.Id);

                if (appointment == null)
                {
                    throw new NotFoundException(nameof(Appointment), request.Id);
                }

                // Actualizar los datos de la cita
                _mapper.Map(request.AppointmentDto, appointment);
                await _appointmentRepository.UpdateAsync(appointment);

                return Unit.Value;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating appointment: {ex.Message}");
                throw;
            }
        }
    }
}