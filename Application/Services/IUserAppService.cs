using Application.Commands.Users;
using Application.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Services
{
    /// <summary>
    /// Defines the contract for managing user-related operations in the application.
    /// </summary>
    public interface IUserAppService
    {
        Task<int> CreateUserAsync(CreateUserCommand command);
        Task UpdateUserAsync(UpdateUserCommand command);
        Task DeleteUserAsync(DeleteUserCommand command);
        Task<IEnumerable<UserDto>> GetAllUsersAsync();
        Task<UserDto> GetUserByIdAsync(int id);
    }
}