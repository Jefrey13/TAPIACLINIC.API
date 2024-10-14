using Application.Commands.Appointments;
using Application.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Services
{
    public interface IAppointmentAppService
    {
        Task<int> CreateAppointmentAsync(CreateAppointmentCommand command);
        Task UpdateAppointmentAsync(UpdateAppointmentCommand command);
        Task DeleteAppointmentAsync(DeleteAppointmentCommand command);
        Task<IEnumerable<AppointmentDto>> GetAllAppointmentsAsync();
        Task<AppointmentDto> GetAppointmentByIdAsync(int id);
    }
}