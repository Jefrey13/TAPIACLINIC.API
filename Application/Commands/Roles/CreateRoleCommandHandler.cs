using Application.Commands.Roles;
using AutoMapper;
using Domain.Entities;
using Domain.Repositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Handlers.Roles
{
    /// <summary>
    /// Handler for creating a new role.
    /// This class is responsible for handling the `CreateRoleCommand`.
    /// It maps the `RoleRequestDto` to a `Role` entity, assigns the related menus and permissions,
    /// and delegates the persistence of the role to the `IRoleRepository`.
    /// </summary>
    public class CreateRoleCommandHandler : IRequestHandler<CreateRoleCommand, int>
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IMapper _mapper;

        public CreateRoleCommandHandler(IRoleRepository roleRepository, IMapper mapper)
        {
            _roleRepository = roleRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Handles the process of creating a role, mapping the DTO to an entity,
        /// assigning related menus and permissions, and saving the role to the database.
        /// </summary>
        /// <param name="request">The create role command containing the role data.</param>
        /// <param name="cancellationToken">Token to cancel the operation.</param>
        /// <returns>The ID of the newly created role.</returns>
        public async Task<int> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
        {
            var role = _mapper.Map<Role>(request.RoleDto);

            // Assign menus and permissions before saving the role
            foreach (var menuId in request.RoleDto.MenuIds)
            {
                role.RoleMenus.Add(new RoleMenu { MenuId = menuId });
            }

            foreach (var permissionId in request.RoleDto.PermissionIds)
            {
                role.RolePermissions.Add(new RolePermission { PermissionId = permissionId });
            }

            // Save the role along with its relations
            await _roleRepository.AddAsync(role);

            return role.Id;
        }
    }
}