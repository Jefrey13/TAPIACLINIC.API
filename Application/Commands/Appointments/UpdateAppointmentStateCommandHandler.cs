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
            try
            {
                // Validar los datos del comando
                if (request.AppointmentId <= 0)
                {
                    throw new ArgumentException("Invalid appointment ID");
                }

                if (string.IsNullOrWhiteSpace(request.NewStateName))
                {
                    throw new ArgumentException("State name is required");
                }

                // Invocar el método del repositorio para actualizar el estado
                return await _appointmentRepository.UpdateAppointmentStateAsync(request.AppointmentId, request.NewStateName);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating appointment state: {ex.Message}");
                throw;
            }
        }
    }
}
