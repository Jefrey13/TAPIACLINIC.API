using Application.Commands.Roles;
using Application.Exceptions;
using AutoMapper;
using Domain.Entities;
using Domain.Repositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Handlers.Roles
{
    /// <summary>
    /// Handler for updating an existing role.
    /// This class handles the `UpdateRoleCommand`.
    /// It retrieves the role, updates its properties and relationships, and saves the changes using `IRoleRepository`.
    /// </summary>
    public class UpdateRoleCommandHandler : IRequestHandler<UpdateRoleCommand, Unit>
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IMapper _mapper;

        public UpdateRoleCommandHandler(IRoleRepository roleRepository, IMapper mapper)
        {
            _roleRepository = roleRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Handles the process of updating a role, including clearing and re-assigning its related menus and permissions.
        /// </summary>
        /// <param name="request">The update role command containing the updated role data.</param>
        /// <param name="cancellationToken">Token to cancel the operation.</param>
        /// <returns>An empty response indicating success.</returns>
        public async Task<Unit> Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
        {
            var role = await _roleRepository.GetByIdAsync(request.Id);
            if (role == null)
            {
                throw new NotFoundException(nameof(Role), request.Id);
            }

            // Update the role with the new DTO data
            _mapper.Map(request.RoleDto, role);

            // Clear and reassign the relationships
            role.RoleMenus.Clear();
            role.RolePermissions.Clear();

            foreach (var menuId in request.RoleDto.MenuIds)
            {
                role.RoleMenus.Add(new RoleMenu { MenuId = menuId });
            }

            foreach (var permissionId in request.RoleDto.PermissionIds)
            {
                role.RolePermissions.Add(new RolePermission { PermissionId = permissionId });
            }

            // Save the updated relationships
            await _roleRepository.UpdateAsync(role);

            return Unit.Value;
        }
    }
}