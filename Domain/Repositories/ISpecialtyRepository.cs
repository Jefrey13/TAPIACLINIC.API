using Domain.Entities;

namespace Domain.Repositories; 
public interface ISpecialtyRepository : IRepository<Specialty>
{
    Task<IEnumerable<Specialty>> GetSpecialtiesByStaffIdAsync(int staffId);
}