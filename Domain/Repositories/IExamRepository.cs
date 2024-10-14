using Domain.Entities;

namespace Domain.Repositories; 
public interface IExamRepository : IRepository<Exam>
{
    Task<IEnumerable<Exam>> GetExamsByPatientIdAsync(int patientId);
}