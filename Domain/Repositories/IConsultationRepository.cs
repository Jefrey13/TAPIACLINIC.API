using Domain.Entities;

namespace Domain.Repositories; 
public interface IConsultationRepository : IRepository<Consultation>
{
    Task<IEnumerable<Consultation>> GetConsultationsByPatientIdAsync(int patientId);
    Task<IEnumerable<Consultation>> GetConsultationsByAppointmentIdAsync(int appointmentId);
}