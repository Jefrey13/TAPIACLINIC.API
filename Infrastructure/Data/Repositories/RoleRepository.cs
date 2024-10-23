using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Data.Repositories
{
    /// <summary>
    /// Repository for managing Role entities.
    /// This class handles CRUD operations for roles, as well as assigning related menus and permissions.
    /// </summary>
    public class RoleRepository : BaseRepository<Role>, IRoleRepository
    {
        private readonly ApplicationDbContext _context;

        public RoleRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        /// <summary>
        /// Retrieves all roles from the database, including their related menus and permissions.
        /// </summary>
        /// <returns>A list of roles with their menus and permissions.</returns>
        public override async Task<IEnumerable<Role>> GetAllAsync()
        {
            return await _context.Roles
                .Include(r => r.RoleMenus)
                    .ThenInclude(rm => rm.Menu)
                .Include(r => r.RolePermissions)
                    .ThenInclude(rp => rp.Permission)
                .ToListAsync();
        }

        /// <summary>
        /// Retrieves a specific role by its ID, including its related menus and permissions.
        /// </summary>
        /// <param name="id">The ID of the role to retrieve.</param>
        /// <returns>The role with its menus and permissions.</returns>
        public override async Task<Role> GetByIdAsync(int id)
        {
            return await _context.Roles
                .Include(r => r.RoleMenus)
                    .ThenInclude(rm => rm.Menu)
                .Include(r => r.RolePermissions)
                    .ThenInclude(rp => rp.Permission)
                .FirstOrDefaultAsync(r => r.Id == id);
        }

        /// <summary>
        /// Adds a new role to the database, ensuring its related menus and permissions are also saved.
        /// </summary>
        /// <param name="role">The role entity to add.</param>
        public override async Task AddAsync(Role role)
        {
            // Ensure related entities are tracked before saving
            foreach (var menu in role.RoleMenus)
            {
                _context.Entry(menu).State = EntityState.Added;
            }

            foreach (var permission in role.RolePermissions)
            {
                _context.Entry(permission).State = EntityState.Added;
            }

            await base.AddAsync(role);
        }

        /// <summary>
        /// Updates an existing role and manages its related menus and permissions.
        /// </summary>
        /// <param name="role">The updated role entity.</param>
        public override async Task UpdateAsync(Role role)
        {
            // Retrieve the current role and its relationships
            var currentRole = await GetByIdAsync(role.Id);

            if (currentRole != null)
            {
                // Remove current relationships
                _context.RoleMenus.RemoveRange(currentRole.RoleMenus);
                _context.RolePermissions.RemoveRange(currentRole.RolePermissions);

                // Assign new relationships
                foreach (var menu in role.RoleMenus)
                {
                    _context.Entry(menu).State = EntityState.Added;
                }

                foreach (var permission in role.RolePermissions)
                {
                    _context.Entry(permission).State = EntityState.Added;
                }
            }

            await base.UpdateAsync(role);
        }

        /// <summary>
        /// Assigns a set of menus to a specific role by its ID.
        /// </summary>
        /// <param name="roleId">The ID of the role to assign menus to.</param>
        /// <param name="menuIds">The collection of menu IDs to assign.</param>
        public async Task AssignMenusToRole(int roleId, IEnumerable<int> menuIds)
        {
            var role = await _context.Roles
                .Include(r => r.RoleMenus)
                .FirstOrDefaultAsync(r => r.Id == roleId);

            if (role != null)
            {
                // Remove current menus and assign new ones
                _context.RoleMenus.RemoveRange(role.RoleMenus);

                foreach (var menuId in menuIds)
                {
                    role.RoleMenus.Add(new RoleMenu { RoleId = roleId, MenuId = menuId });
                }

                await _context.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Assigns a set of permissions to a specific role by its ID.
        /// </summary>
        /// <param name="roleId">The ID of the role to assign permissions to.</param>
        /// <param name="permissionIds">The collection of permission IDs to assign.</param>
        public async Task AssignPermissionsToRole(int roleId, IEnumerable<int> permissionIds)
        {
            var role = await _context.Roles
                .Include(r => r.RolePermissions)
                .FirstOrDefaultAsync(r => r.Id == roleId);

            if (role != null)
            {
                // Remove current permissions and assign new ones
                _context.RolePermissions.RemoveRange(role.RolePermissions);

                foreach (var permissionId in permissionIds)
                {
                    role.RolePermissions.Add(new RolePermission { RoleId = roleId, PermissionId = permissionId });
                }

                await _context.SaveChangesAsync();
            }
        }
        public async Task<int?> GetRoleIdByNameAsync(string roleName)
        {
            var role = await _context.Roles
                .Where(r => r.Name == roleName)
                .FirstOrDefaultAsync();

            return role?.Id;
        }
    }
}