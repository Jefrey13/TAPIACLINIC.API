using Domain.Entities;

namespace Domain.Repositories; 
public interface IMedicalRecordRepository : IRepository<MedicalRecord>
{
    Task<MedicalRecord> GetMedicalRecordByPatientIdAsync(int patientId);
}