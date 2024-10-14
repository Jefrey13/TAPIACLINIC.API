using Application.Commands.Permissions;
using Application.Exceptions;
using AutoMapper;
using Domain.Entities;
using Domain.Repositories;
using MediatR;

namespace Application.Handlers.Permissions
{
    /// <summary>
    /// Handler for updating a permission.
    /// </summary>
    public class UpdatePermissionCommandHandler : IRequestHandler<UpdatePermissionCommand, Unit>
    {
        private readonly IPermissionRepository _permissionRepository;
        private readonly IMapper _mapper;

        public UpdatePermissionCommandHandler(IPermissionRepository permissionRepository, IMapper mapper)
        {
            _permissionRepository = permissionRepository;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdatePermissionCommand request, CancellationToken cancellationToken)
        {
            var permission = await _permissionRepository.GetByIdAsync(request.Id);
            if (permission == null)
            {
                throw new NotFoundException(nameof(Permission), request.Id);
            }

            _mapper.Map(request.PermissionDto, permission);
            permission.UpdatedAt = DateTime.Now;

            await _permissionRepository.UpdateAsync(permission);
            return Unit.Value;
        }
    }
}