using Application.Exceptions;
using Application.Models;
using Application.Queries.Permissions;
using AutoMapper;
using Domain.Entities;
using Domain.Repositories;
using MediatR;

namespace Application.Handlers.Permissions
{
    /// <summary>
    /// Handler for retrieving a permission by its ID.
    /// </summary>
    public class GetPermissionByIdQueryHandler : IRequestHandler<GetPermissionByIdQuery, PermissionDto>
    {
        private readonly IPermissionRepository _permissionRepository;
        private readonly IMapper _mapper;

        public GetPermissionByIdQueryHandler(IPermissionRepository permissionRepository, IMapper mapper)
        {
            _permissionRepository = permissionRepository;
            _mapper = mapper;
        }

        public async Task<PermissionDto> Handle(GetPermissionByIdQuery request, CancellationToken cancellationToken)
        {
            var permission = await _permissionRepository.GetByIdAsync(request.Id);
            if (permission == null)
            {
                throw new NotFoundException(nameof(Permission), request.Id);
            }

            return _mapper.Map<PermissionDto>(permission);
        }
    }
}