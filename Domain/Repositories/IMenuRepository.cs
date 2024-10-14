using Domain.Entities;

namespace Domain.Repositories; 
public interface IMenuRepository : IRepository<Menu>
{
    Task<IEnumerable<Menu>> GetMenusByParentIdAsync(int? parentId);
    Task<IEnumerable<Menu>> GetMenusByRoleIdAsync(int roleId);
}