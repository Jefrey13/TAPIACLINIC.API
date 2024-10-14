using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repositories;

public class ExamRepository : BaseRepository<Exam>, IExamRepository
{
    public ExamRepository(ApplicationDbContext context) : base(context) { }

    public async Task<IEnumerable<Exam>> GetExamsByPatientIdAsync(int patientId)
    {
        return await _context.Exams
            .Where(e => e.ConsultationExams.Any(ce => ce.Consultation.MedicalRecord.PatientId == patientId))
            .ToListAsync();
    }
}