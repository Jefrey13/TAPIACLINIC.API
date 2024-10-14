using Application.Commands.Staffs;
using Application.Models;

namespace Application.Services
{
    public interface IStaffAppService
    {
        Task<int> CreateStaffAsync(CreateStaffCommand command);
        Task UpdateStaffAsync(UpdateStaffCommand command);
        Task DeleteStaffAsync(int id);
        Task<IEnumerable<StaffDto>> GetAllStaffsAsync();
        Task<StaffDto> GetStaffByIdAsync(int id);
    }
}