using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Data.Repositories
{
    public class RoleRepository : BaseRepository<Role>, IRoleRepository
    {
        private readonly ApplicationDbContext _context;

        public RoleRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        // Fetch roles associated with a specific user, including menus and permissions
        public async Task<IEnumerable<Role>> GetRolesByUserIdAsync(int userId)
        {
            return await _context.Roles
                .Include(r => r.RoleMenus)
                .Include(r => r.RolePermissions)
                .ToListAsync();
        }

        // Method to assign menus to a role
        public async Task AssignMenusToRole(int roleId, IEnumerable<int> menuIds)
        {
            var role = await _context.Roles
                .Include(r => r.RoleMenus)
                .FirstOrDefaultAsync(r => r.Id == roleId);

            if (role != null)
            {
                // Remove existing menus
                _context.RoleMenus.RemoveRange(role.RoleMenus);

                // Assign new menus
                foreach (var menuId in menuIds)
                {
                    role.RoleMenus.Add(new RoleMenu { RoleId = roleId, MenuId = menuId });
                }

                await _context.SaveChangesAsync();
            }
        }

        // Method to assign permissions to a role
        public async Task AssignPermissionsToRole(int roleId, IEnumerable<int> permissionIds)
        {
            var role = await _context.Roles
                .Include(r => r.RolePermissions)
                .FirstOrDefaultAsync(r => r.Id == roleId);

            if (role != null)
            {
                // Remove existing permissions
                _context.RolePermissions.RemoveRange(role.RolePermissions);

                // Assign new permissions
                foreach (var permissionId in permissionIds)
                {
                    role.RolePermissions.Add(new RolePermission { RoleId = roleId, PermissionId = permissionId });
                }

                await _context.SaveChangesAsync();
            }
        }
    }
}