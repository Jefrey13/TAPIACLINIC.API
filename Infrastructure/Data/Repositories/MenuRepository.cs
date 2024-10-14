using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repositories;

public class MenuRepository : BaseRepository<Menu>, IMenuRepository
{
    public MenuRepository(ApplicationDbContext context) : base(context) { }

    public async Task<IEnumerable<Menu>> GetMenusByParentIdAsync(int? parentId)
    {
        return await _context.Menus
            .Where(m => m.ParentId == parentId)
            .ToListAsync();
    }

    public async Task<IEnumerable<Menu>> GetMenusByRoleIdAsync(int roleId)
    {
        return await _context.Menus
            .Where(m => m.RoleMenus.Any(rm => rm.RoleId == roleId))
            .ToListAsync();
    }
}