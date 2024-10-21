using Domain.Entities;

namespace Domain.Repositories; 
public interface IScheduleRepository : IRepository<Schedule>
    {
        //Task<IEnumerable<Specialty>> GetSchedulesBySpecialtyAsync(int staffId);
    }