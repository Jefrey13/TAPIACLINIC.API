using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repositories;

public class ConsultationRepository : BaseRepository<Consultation>, IConsultationRepository
{
    public ConsultationRepository(ApplicationDbContext context) : base(context) { }

    public async Task<IEnumerable<Consultation>> GetConsultationsByPatientIdAsync(int patientId)
    {
        return await _context.Consultations
            .Where(c => c.MedicalRecord.PatientId == patientId)
            .ToListAsync();
    }

    public async Task<IEnumerable<Consultation>> GetConsultationsByAppointmentIdAsync(int appointmentId)
    {
        return await _context.Consultations
            .Where(c => c.AppointmentId == appointmentId)
            .ToListAsync();
    }
}