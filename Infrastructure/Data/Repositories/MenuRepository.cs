using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repositories
{
    /// <summary>
    /// Repository implementation for the Menu entity.
    /// Provides methods to retrieve menus based on parent-child relationships and roles.
    /// </summary>
    public class MenuRepository : BaseRepository<Menu>, IMenuRepository
    {
        /// <summary>
        /// Initializes a new instance of the MenuRepository class with the specified DbContext.
        /// </summary>
        /// <param name="context">The application database context.</param>
        public MenuRepository(ApplicationDbContext context) : base(context) { }

        /// <summary>
        /// Retrieves menus by their parent ID. This is used to fetch submenus for a given parent menu.
        /// </summary>
        /// <param name="parentId">The ID of the parent menu. If null, returns root-level menus.</param>
        /// <returns>A collection of menus that have the specified parent ID.</returns>
        public async Task<IEnumerable<Menu>> GetMenusByParentIdAsync(int? parentId)
        {
            return await _context.Menus
                .Where(m => m.ParentId == parentId)
                .ToListAsync();
        }

        /// <summary>
        /// Retrieves top-level menus associated with a specific role ID, and recursively loads their submenus.
        /// </summary>
        /// <param name="roleId">The ID of the role for which to fetch menus.</param>
        /// <returns>A collection of top-level menus, including their recursive submenus.</returns>
        public async Task<IEnumerable<Menu>> GetMenusByRoleIdAsync(int roleId)
        {
            // Fetch top-level (parent) menus that belong to the specified role and have no parent (ParentId is null)
            var parentMenus = await _context.Menus
                .Where(m => m.RoleMenus.Any(rm => rm.RoleId == roleId) && m.ParentId == null)
                .ToListAsync();

            // For each parent menu, recursively load its submenus (children)
            foreach (var menu in parentMenus)
            {
                await LoadSubMenusAsync(menu);
            }

            return parentMenus;
        }

        /// <summary>
        /// Recursively loads submenus (children) for a given parent menu.
        /// </summary>
        /// <param name="parentMenu">The parent menu for which to load submenus.</param>
        private async Task LoadSubMenusAsync(Menu parentMenu)
        {
            // Fetch submenus that have the current menu as their parent (based on ParentId)
            var subMenus = await _context.Menus
                .Where(m => m.ParentId == parentMenu.Id)
                .ToListAsync();

            // Base case: if no submenus exist, return
            if (!subMenus.Any())
            {
                return;
            }

            // Assign the found submenus as children of the parent menu
            parentMenu.Children = subMenus;

            // Recursively load submenus for each submenu
            foreach (var subMenu in subMenus)
            {
                await LoadSubMenusAsync(subMenu);
            }
        }
    }
}