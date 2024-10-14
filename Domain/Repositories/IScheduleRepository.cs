using Domain.Entities;

namespace Domain.Repositories; 
public interface IScheduleRepository : IRepository<Schedule>
    {
        Task<IEnumerable<Schedule>> GetSchedulesByStaffIdAsync(int staffId);
    }