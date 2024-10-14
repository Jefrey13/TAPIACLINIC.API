using Application.Models;
using Application.Queries.Permissions;
using AutoMapper;
using Domain.Repositories;
using MediatR;

namespace Application.Handlers.Permissions
{
    /// <summary>
    /// Handler for retrieving all permissions.
    /// </summary>
    public class GetAllPermissionsQueryHandler : IRequestHandler<GetAllPermissionsQuery, IEnumerable<PermissionDto>>
    {
        private readonly IPermissionRepository _permissionRepository;
        private readonly IMapper _mapper;

        public GetAllPermissionsQueryHandler(IPermissionRepository permissionRepository, IMapper mapper)
        {
            _permissionRepository = permissionRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PermissionDto>> Handle(GetAllPermissionsQuery request, CancellationToken cancellationToken)
        {
            var permissions = await _permissionRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<PermissionDto>>(permissions);
        }
    }
}