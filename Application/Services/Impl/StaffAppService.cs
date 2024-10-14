using Application.Commands.Staffs;
using Application.Models;
using Application.Queries.Staffs;
using MediatR;

namespace Application.Services.Impl
{
    public class StaffAppService : IStaffAppService
    {
        private readonly IMediator _mediator;

        public StaffAppService(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<int> CreateStaffAsync(CreateStaffCommand command)
        {
            return await _mediator.Send(command);
        }

        public async Task UpdateStaffAsync(UpdateStaffCommand command)
        {
            await _mediator.Send(command);
        }

        public async Task DeleteStaffAsync(int id)
        {
            await _mediator.Send(new DeleteStaffCommand(id));
        }

        public async Task<IEnumerable<StaffDto>> GetAllStaffsAsync()
        {
            return await _mediator.Send(new GetAllStaffsQuery());
        }

        public async Task<StaffDto> GetStaffByIdAsync(int id)
        {
            return await _mediator.Send(new GetStaffByIdQuery(id));
        }
    }
}