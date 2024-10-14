using Application.Models;
using MediatR;

namespace Application.Commands.Roles
{
    /// <summary>
    /// Command to update an existing role.
    /// </summary>
    public class UpdateRoleCommand : IRequest<Unit>
    {
        public int Id { get; set; }
        public RoleDto RoleDto { get; set; }

        public UpdateRoleCommand(int id, RoleDto roleDto)
        {
            Id = id;
            RoleDto = roleDto;
        }
    }
}