using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Data.Repositories
{
    public class RecordSurgeryRepository : BaseRepository<RecordSurgery>, IRecordSurgeryRepository
    {
        public RecordSurgeryRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<RecordSurgery>> GetSurgeriesByMedicalRecordIdAsync(int medicalRecordId)
        {
            return await _context.RecordSurgeries
                .Where(rs => rs.RecordId == medicalRecordId)
                .ToListAsync();
        }

        public async Task<IEnumerable<RecordSurgery>> GetSurgeriesByConsultationIdAsync(int consultationId)
        {
            return await _context.RecordSurgeries
                .Where(rs => rs.ConsultationId == consultationId)
                .ToListAsync();
        }
    }
}