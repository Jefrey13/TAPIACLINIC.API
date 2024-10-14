using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Data.Repositories
{
    public class ConsultationExamRepository : BaseRepository<ConsultationExam>, IConsultationExamRepository
    {
        public ConsultationExamRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<ConsultationExam>> GetExamsByConsultationIdAsync(int consultationId)
        {
            return await _context.ConsultationExams
                .Where(ce => ce.ConsultationId == consultationId)
                .ToListAsync();
        }

        public async Task<IEnumerable<ConsultationExam>> GetConsultationsByExamIdAsync(int examId)
        {
            return await _context.ConsultationExams
                .Where(ce => ce.ExamId == examId)
                .ToListAsync();
        }
    }
}