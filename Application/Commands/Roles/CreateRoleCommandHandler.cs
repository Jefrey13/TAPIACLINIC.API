using Application.Commands.Roles;
using AutoMapper;
using Domain.Entities;
using Domain.Repositories;
using MediatR;

namespace Application.Handlers.Roles
{
    /// <summary>
    /// Handler for creating a new role, along with its permissions and menus.
    /// </summary>
    public class CreateRoleCommandHandler : IRequestHandler<CreateRoleCommand, int>
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IPermissionRepository _permissionRepository;
        private readonly IMenuRepository _menuRepository;
        private readonly IMapper _mapper;

        public CreateRoleCommandHandler(
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

        public async Task<int> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
        {
            var role = _mapper.Map<Role>(request.RoleDto);
            role.CreatedAt = DateTime.Now;
            role.UpdatedAt = DateTime.Now;

            // Add associated permissions
            foreach (var permissionId in request.RoleDto.PermissionIds)
            {
                var permission = await _permissionRepository.GetByIdAsync(permissionId);
                if (permission != null)
                {
                    role.RolePermissions.Add(new RolePermission { PermissionId = permissionId });
                }
            }

            // Add associated menus
            foreach (var menuId in request.RoleDto.MenuIds)
            {
                var menu = await _menuRepository.GetByIdAsync(menuId);
                if (menu != null)
                {
                    role.RoleMenus.Add(new RoleMenu { MenuId = menuId });
                }
            }

            await _roleRepository.AddAsync(role);
            return role.Id;
        }
    }
}