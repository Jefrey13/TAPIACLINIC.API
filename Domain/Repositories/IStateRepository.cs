using Domain.Entities;

namespace Domain.Repositories;
public interface IStateRepository : IRepository<State>
{
    Task<IEnumerable<State>> GetStatesByTypeAsync(string stateType);
}