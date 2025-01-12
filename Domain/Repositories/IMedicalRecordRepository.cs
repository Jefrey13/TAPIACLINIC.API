using Domain.Entities;

namespace Domain.Repositories; 
public interface IMedicalRecordRepository : IRepository<MedicalRecord>
{
    Task<List<MedicalRecord>> GetMedicalRecordByPatientIdAsync(int patientId);
}