using Application.Models;
using MediatR;

namespace Application.Commands.Permissions
{
    /// <summary>
    /// Command to update an existing permission.
    /// </summary>
    public class UpdatePermissionCommand : IRequest<Unit>
    {
        public int Id { get; set; }
        public PermissionDto PermissionDto { get; set; }

        public UpdatePermissionCommand(int id, PermissionDto permissionDto)
        {
            Id = id;
            PermissionDto = permissionDto;
        }
    }
}