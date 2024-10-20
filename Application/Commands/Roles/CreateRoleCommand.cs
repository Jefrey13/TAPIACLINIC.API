using Application.Models.RequestDtos;
using MediatR;

namespace Application.Commands.Roles
{
    /// <summary>
    /// Command to create a new role.
    /// </summary>
    public class CreateRoleCommand : IRequest<int>
    {
        public RoleRequestDto RoleDto { get; set; }

        public CreateRoleCommand(RoleRequestDto roleDto)
        {
            RoleDto = roleDto;
        }
    }
}