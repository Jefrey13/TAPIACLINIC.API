using Application.Models;
using MediatR;

namespace Application.Commands.Permissions
{
    /// <summary>
    /// Command to create a new permission.
    /// </summary>
    public class CreatePermissionCommand : IRequest<int>
    {
        public PermissionDto PermissionDto { get; set; }

        public CreatePermissionCommand(PermissionDto permissionDto)
        {
            PermissionDto = permissionDto;
        }
    }
}