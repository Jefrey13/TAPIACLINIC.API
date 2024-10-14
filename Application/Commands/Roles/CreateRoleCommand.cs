using Application.Models;
using MediatR;

namespace Application.Commands.Roles
{
    /// <summary>
    /// Command to create a new role.
    /// </summary>
    public class CreateRoleCommand : IRequest<int>
    {
        public RoleDto RoleDto { get; set; }

        public CreateRoleCommand(RoleDto roleDto)
        {
            RoleDto = roleDto;
        }
    }
}