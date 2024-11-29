using Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.Appointments
{
    public class UpdateAppointmentStateCommandHandler : IRequestHandler<UpdateAppointmentStateCommand, bool>
    {
        private readonly IAppointmentRepository _appointmentRepository;

        public UpdateAppointmentStateCommandHandler(IAppointmentRepository appointmentRepository)
        {
            _appointmentRepository = appointmentRepository;
        }

        public async Task<bool> Handle(UpdateAppointmentStateCommand request, CancellationToken cancellationToken)
        {
            // Invocar el método del repositorio para actualizar el estado
            return await _appointmentRepository.UpdateAppointmentStateAsync(request.AppointmentId, request.NewStateName);
        }
    }
}
