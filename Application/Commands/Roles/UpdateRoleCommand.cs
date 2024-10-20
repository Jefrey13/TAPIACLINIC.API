using Application.Models.RequestDtos;
using MediatR;

namespace Application.Commands.Roles
{
    /// <summary>
    /// Command to update an existing role.
    /// </summary>
    public class UpdateRoleCommand : IRequest<Unit>
    {
        public int Id { get; set; }
        public RoleRequestDto RoleDto { get; set; }

        public UpdateRoleCommand(int id, RoleRequestDto roleDto)
        {
            Id = id;
            RoleDto = roleDto;
        }
    }
}