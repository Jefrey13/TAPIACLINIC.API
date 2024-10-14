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
            var appointment = _mapper.Map<Appointment>(request.AppointmentDto);
            await _appointmentRepository.AddAsync(appointment);
            return appointment.Id;
        }
    }
}