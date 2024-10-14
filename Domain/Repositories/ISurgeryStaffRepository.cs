using Domain.Entities;

namespace Domain.Repositories; 
public interface ISurgeryStaffRepository : IRepository<SurgeryStaff>
{
    Task<IEnumerable<SurgeryStaff>> GetStaffBySurgeryIdAsync(int surgeryId);
}