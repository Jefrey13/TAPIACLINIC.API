using Domain.Entities;

namespace Domain.Repositories; 
public interface IScheduleRepository : IRepository<Schedule>
    {
        Task<IEnumerable<Schedule>> GetSchedulesBySpecialtyAsync(int specialtyId);
    }