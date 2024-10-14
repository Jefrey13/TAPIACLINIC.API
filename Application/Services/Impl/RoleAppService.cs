using Application.Commands.Roles;
using Application.Models;
using Application.Queries.Roles;
using AutoMapper;
using MediatR;

namespace Application.Services.Impl
{
    /// <summary>
    /// Implements operations related to roles.
    /// </summary>
    public class RoleAppService : IRoleAppService
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public RoleAppService(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        public async Task<int> CreateRoleAsync(RoleDto roleDto)
        {
            var command = new CreateRoleCommand(roleDto);
            return await _mediator.Send(command);
        }

        public async Task UpdateRoleAsync(int id, RoleDto roleDto)
        {
            var command = new UpdateRoleCommand(id, roleDto);
            await _mediator.Send(command);
        }

        public async Task DeleteRoleAsync(int id)
        {
            var command = new DeleteRoleCommand(id);
            await _mediator.Send(command);
        }

        public async Task<IEnumerable<RoleDto>> GetAllRolesAsync()
        {
            return await _mediator.Send(new GetAllRolesQuery());
        }

        public async Task<RoleDto> GetRoleByIdAsync(int id)
        {
            return await _mediator.Send(new GetRoleByIdQuery(id));
        }
    }
}