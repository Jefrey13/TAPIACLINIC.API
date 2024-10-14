using Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Repositories
{
    public interface IRoleRepository : IRepository<Role>
    {
        /// <summary>
        /// Retrieves roles associated with a specific user.
        /// </summary>
        /// <param name="userId">The ID of the user.</param>
        /// <returns>A collection of roles assigned to the user.</returns>
        Task<IEnumerable<Role>> GetRolesByUserIdAsync(int userId);

        /// <summary>
        /// Assigns a set of menus to a role.
        /// </summary>
        /// <param name="roleId">The ID of the role to update.</param>
        /// <param name="menuIds">A collection of menu IDs to assign to the role.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task AssignMenusToRole(int roleId, IEnumerable<int> menuIds);

        /// <summary>
        /// Assigns a set of permissions to a role.
        /// </summary>
        /// <param name="roleId">The ID of the role to update.</param>
        /// <param name="permissionIds">A collection of permission IDs to assign to the role.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task AssignPermissionsToRole(int roleId, IEnumerable<int> permissionIds);
    }
}