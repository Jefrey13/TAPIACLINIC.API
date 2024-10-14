using Domain.Entities;

namespace Domain.Repositories;
public interface IPermissionRepository : IRepository<Permission>
    {
        Task<IEnumerable<Permission>> GetPermissionsByRoleIdAsync(int roleId);
    }