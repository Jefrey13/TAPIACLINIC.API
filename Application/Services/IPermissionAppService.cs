using Application.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Services
{
    public interface IPermissionAppService
    {
        Task<int> CreatePermissionAsync(PermissionDto permissionDto);
        Task UpdatePermissionAsync(int id, PermissionDto permissionDto);
        Task DeletePermissionAsync(int id);
        Task<PermissionDto> GetPermissionByIdAsync(int id);
        Task<IEnumerable<PermissionDto>> GetAllPermissionsAsync();
    }
}