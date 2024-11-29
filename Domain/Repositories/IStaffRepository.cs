using Domain.Entities;

namespace Domain.Repositories; 
public interface IStaffRepository : IRepository<Staff>
{
    Task<Staff> GetStaffByUserIdAsync(int userId);
    Task<IEnumerable<Staff>> GetStaffBySpecialtyIdAsync(int specialtyId);

    Task<IEnumerable<Staff>> GetByStateAsync(int stateId);

    Task<IEnumerable<Staff>> GetByRoleAsync(string roleName);
}