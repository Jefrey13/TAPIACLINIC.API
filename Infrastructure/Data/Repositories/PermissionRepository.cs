using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repositories;

public class PermissionRepository : BaseRepository<Permission>, IPermissionRepository
{
    public PermissionRepository(ApplicationDbContext context) : base(context) { }

    public async Task<IEnumerable<Permission>> GetPermissionsByRoleIdAsync(int roleId)
    {
        return await _context.Permissions
            .Where(p => p.RolePermissions.Any(rp => rp.RoleId == roleId))
            .ToListAsync();
    }
}