using Application.Commands.Roles;
using Application.Models;
using Application.Models.ReponseDtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Services
{
    public interface IRoleAppService
    {
        Task<int> CreateRoleAsync(CreateRoleCommand command);
        Task UpdateRoleAsync(int id, UpdateRoleCommand command);
        Task DeleteRoleAsync(DeleteRoleCommand command);
        Task<RoleResponseDto> GetRoleByIdAsync(int id);
        Task<IEnumerable<RoleResponseDto>> GetAllRolesAsync();
    }
}