using Application.Commands.Permissions;
using AutoMapper;
using Domain.Entities;
using Domain.Repositories;
using MediatR;

namespace Application.Handlers.Permissions
{
    /// <summary>
    /// Handler for creating a new permission.
    /// </summary>
    public class CreatePermissionCommandHandler : IRequestHandler<CreatePermissionCommand, int>
    {
        private readonly IPermissionRepository _permissionRepository;
        private readonly IMapper _mapper;

        public CreatePermissionCommandHandler(IPermissionRepository permissionRepository, IMapper mapper)
        {
            _permissionRepository = permissionRepository;
            _mapper = mapper;
        }

        public async Task<int> Handle(CreatePermissionCommand request, CancellationToken cancellationToken)
        {
            var permission = _mapper.Map<Permission>(request.PermissionDto);
            permission.CreatedAt = DateTime.Now;
            permission.UpdatedAt = DateTime.Now;

            await _permissionRepository.AddAsync(permission);
            return permission.Id;
        }
    }
}