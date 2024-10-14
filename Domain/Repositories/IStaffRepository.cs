using Domain.Entities;

namespace Domain.Repositories; 
public interface IStaffRepository : IRepository<Staff>
{
    Task<Staff> GetStaffByUserIdAsync(int userId);
    Task<IEnumerable<Staff>> GetStaffBySpecialtyIdAsync(int specialtyId);
}