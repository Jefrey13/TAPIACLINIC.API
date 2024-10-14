using Application.Commands.Permissions;
using Application.Exceptions;
using Domain.Entities;
using Domain.Repositories;
using MediatR;

namespace Application.Handlers.Permissions
{
    /// <summary>
    /// Handler for deleting a permission by its ID.
    /// </summary>
    public class DeletePermissionCommandHandler : IRequestHandler<DeletePermissionCommand, Unit>
    {
        private readonly IPermissionRepository _permissionRepository;

        public DeletePermissionCommandHandler(IPermissionRepository permissionRepository)
        {
            _permissionRepository = permissionRepository;
        }

        public async Task<Unit> Handle(DeletePermissionCommand request, CancellationToken cancellationToken)
        {
            var permission = await _permissionRepository.GetByIdAsync(request.Id);
            if (permission == null)
            {
                throw new NotFoundException(nameof(Permission), request.Id);
            }

            await _permissionRepository.DeleteAsync(permission);
            return Unit.Value;
        }
    }
}