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
            var appointment = await _appointmentRepository.GetByIdAsync(request.Id);

            if (appointment == null)
            {
                throw new NotFoundException(nameof(Appointment), request.Id);
            }

            _mapper.Map(request.AppointmentDto, appointment);
            await _appointmentRepository.UpdateAsync(appointment);

            return Unit.Value;
        }
    }
}