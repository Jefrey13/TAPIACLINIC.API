using Application.Commands.Appointments;
using AutoMapper;
using Domain.Entities;
using Domain.Repositories;
using MediatR;

namespace Application.Handlers.Appointments
{
    /// <summary>
    /// Handler for creating a new appointment.
    /// </summary>
    public class CreateAppointmentCommandHandler : IRequestHandler<CreateAppointmentCommand, int>
    {
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IMapper _mapper;

        public CreateAppointmentCommandHandler(IAppointmentRepository appointmentRepository, IMapper mapper)
        {
            _appointmentRepository = appointmentRepository;
            _mapper = mapper;
        }

        public async Task<int> Handle(CreateAppointmentCommand request, CancellationToken cancellationToken)
        {
            try
            {
                // Validar si los datos del DTO son válidos
                if (request.AppointmentDto == null)
                {
                    throw new ArgumentException("Appointment data is required");
                }

                var appointment = _mapper.Map<Appointment>(request.AppointmentDto);

                // Validar si los IDs relacionados son válidos
                if (appointment.PatientId <= 0 || appointment.StaffId <= 0 || appointment.SpecialtyId <= 0 || appointment.ScheduleId <= 0)
                {
                    throw new ArgumentException("Invalid related entity IDs");
                }

                await _appointmentRepository.AddAsync(appointment);
                return appointment.Id;
            }
            catch (Exception ex)
            {
                // Loguear el error (puede reemplazarse con un servicio de logging)
                Console.WriteLine($"Error creating appointment: {ex.Message}");
                throw;
            }
        }
    }
}