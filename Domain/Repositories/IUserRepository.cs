using Domain.Entities;

namespace Domain.Repositories; 
public interface IUserRepository : IRepository<User>
{
    Task<User> GetUserByEmailAsync(string email);

    Task<User> GetUserByUserNameAsync(string userName);
    Task UpdatePasswordAsync(User user);

    Task<IEnumerable<User>> GetByStateAsync(int stateId);

    Task<bool> UpdateUserIsAccountActivated(string email);

    Task CreateAdminUserAsync();

    // Implementación específica para GetAllAsync con idRole
    Task<IEnumerable<User>> GetAllAsync(int? idRole = null, int id = 0);
}