using Application.Commands.Roles;
using Application.Exceptions;
using AutoMapper;
using Domain.Entities;
using Domain.Repositories;
using MediatR;

namespace Application.Handlers.Roles
{
    /// <summary>
    /// Handler for updating a role, including its permissions and menus.
    /// </summary>
    public class UpdateRoleCommandHandler : IRequestHandler<UpdateRoleCommand, Unit>
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IPermissionRepository _permissionRepository;
        private readonly IMenuRepository _menuRepository;
        private readonly IMapper _mapper;

        public UpdateRoleCommandHandler(
            IRoleRepository roleRepository,
            IPermissionRepository permissionRepository,
            IMenuRepository menuRepository,
            IMapper mapper)
        {
            _roleRepository = roleRepository;
            _permissionRepository = permissionRepository;
            _menuRepository = menuRepository;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
        {
            var role = await _roleRepository.GetByIdAsync(request.Id);
            if (role == null)
            {
                throw new NotFoundException(nameof(Role), request.Id);
            }

            // Map the updated details to the role
            _mapper.Map(request.RoleDto, role);
            role.UpdatedAt = DateTime.Now;

            // Clear current permissions and menus
            role.RolePermissions.Clear();
            role.RoleMenus.Clear();

            // Update associated permissions
            foreach (var permissionId in request.RoleDto.PermissionIds)
            {
                var permission = await _permissionRepository.GetByIdAsync(permissionId);
                if (permission != null)
                {
                    role.RolePermissions.Add(new RolePermission { PermissionId = permissionId });
                }
            }

            // Update associated menus
            foreach (var menuId in request.RoleDto.MenuIds)
            {
                var menu = await _menuRepository.GetByIdAsync(menuId);
                if (menu != null)
                {
                    role.RoleMenus.Add(new RoleMenu { MenuId = menuId });
                }
            }

            await _roleRepository.UpdateAsync(role);
            return Unit.Value;
        }
    }
}