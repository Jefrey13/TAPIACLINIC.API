using Application.Commands.Appointments;
using Application.Models.RequestDtos;
using Application.Models.ResponseDtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Services
{
    /// <summary>
    /// Service interface for managing appointments.
    /// </summary>
    public interface IAppointmentAppService
    {
        /// <summary>
        /// Creates a new appointment.
        /// </summary>
        /// <param name="command">The command containing the appointment details.</param>
        /// <param name="jwtToken">The JWT token for extracting the user information.</param>
        /// <returns>The ID of the created appointment.</returns>
        Task<int> CreateAppointmentAsync(CreateAppointmentCommand command, string jwtToken);

        /// <summary>
        /// Updates an existing appointment.
        /// </summary>
        /// <param name="command">The command containing the updated appointment details.</param>
        /// <param name="jwtToken">The JWT token for extracting the user information.</param>
        Task UpdateAppointmentAsync(UpdateAppointmentCommand command, string jwtToken);

        /// <summary>
        /// Deletes an appointment by its ID.
        /// </summary>
        /// <param name="command">The command containing the appointment ID.</param>
        Task DeleteAppointmentAsync(DeleteAppointmentCommand command);

        /// <summary>
        /// Retrieves all appointments.
        /// </summary>
        /// <returns>A list of all appointment DTOs.</returns>
        Task<IEnumerable<AppointmentResponseDto>> GetAllAppointmentsAsync();

        /// <summary>
        /// Retrieves an appointment by its ID.
        /// </summary>
        /// <param name="id">The ID of the appointment to retrieve.</param>
        /// <returns>The appointment DTO.</returns>
        Task<AppointmentResponseDto> GetAppointmentByIdAsync(int id);
    }
}