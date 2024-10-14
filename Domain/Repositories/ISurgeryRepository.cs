using Domain.Entities;

namespace Domain.Repositories; 
public interface ISurgeryRepository : IRepository<Surgery>
{
    Task<IEnumerable<Surgery>> GetSurgeriesByPatientIdAsync(int patientId);
}