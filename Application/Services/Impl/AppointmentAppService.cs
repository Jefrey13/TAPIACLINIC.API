using Application.Commands.Appointments;
using Application.Models;
using Application.Queries.Appointments;
using MediatR;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Services.Impl
{
    public class AppointmentAppService : IAppointmentAppService
    {
        private readonly IMediator _mediator;

        public AppointmentAppService(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<int> CreateAppointmentAsync(CreateAppointmentCommand command)
        {
            return await _mediator.Send(command);
        }

        public async Task UpdateAppointmentAsync(UpdateAppointmentCommand command)
        {
            await _mediator.Send(command);
        }

        public async Task DeleteAppointmentAsync(DeleteAppointmentCommand command)
        {
            await _mediator.Send(command);
        }

        public async Task<IEnumerable<AppointmentDto>> GetAllAppointmentsAsync()
        {
            return await _mediator.Send(new GetAllAppointmentsQuery());
        }

        public async Task<AppointmentDto> GetAppointmentByIdAsync(int id)
        {
            return await _mediator.Send(new GetAppointmentByIdQuery(id));
        }
    }
}