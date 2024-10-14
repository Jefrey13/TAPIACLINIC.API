using Application.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Services
{
    /// <summary>
    /// Interface for Role application service.
    /// </summary>
    public interface IRoleAppService
    {
        Task<int> CreateRoleAsync(RoleDto roleDto);
        Task UpdateRoleAsync(int id, RoleDto roleDto);
        Task DeleteRoleAsync(int id);
        Task<IEnumerable<RoleDto>> GetAllRolesAsync();
        Task<RoleDto> GetRoleByIdAsync(int id);
    }
}