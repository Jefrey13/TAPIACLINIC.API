using Application.Commands.Permissions;
using Application.Models;
using Application.Queries.Permissions;
using AutoMapper;
using MediatR;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Services.Impl
{
    public class PermissionAppService : IPermissionAppService
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public PermissionAppService(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        public async Task<int> CreatePermissionAsync(PermissionDto permissionDto)
        {
            return await _mediator.Send(new CreatePermissionCommand(permissionDto));
        }

        public async Task UpdatePermissionAsync(int id, PermissionDto permissionDto)
        {
            await _mediator.Send(new UpdatePermissionCommand(id, permissionDto));
        }

        public async Task DeletePermissionAsync(int id)
        {
            await _mediator.Send(new DeletePermissionCommand(id));
        }

        public async Task<PermissionDto> GetPermissionByIdAsync(int id)
        {
            return await _mediator.Send(new GetPermissionByIdQuery(id));
        }

        public async Task<IEnumerable<PermissionDto>> GetAllPermissionsAsync()
        {
            return await _mediator.Send(new GetAllPermissionsQuery());
        }
    }
}