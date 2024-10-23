using Application.Commands.Appointments;
using Application.Models.RequestDtos;
using Application.Services;
using API.Utils;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Models.ResponseDtos;

namespace API.Controllers
{
    /// <summary>
    /// Controller for managing appointments.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentsController : ControllerBase
    {
        private readonly IAppointmentAppService _appointmentAppService;

        /// <summary>
        /// Initializes a new instance of the <see cref="AppointmentsController"/> class.
        /// </summary>
        /// <param name="appointmentAppService">The appointment service to manage appointments.</param>
        public AppointmentsController(IAppointmentAppService appointmentAppService)
        {
            _appointmentAppService = appointmentAppService;
        }

        /// <summary>
        /// Retrieves all appointments.
        /// </summary>
        /// <returns>A list of all appointments.</returns>
        [HttpGet]
        public async Task<ActionResult<ApiResponse<IEnumerable<AppointmentResponseDto>>>> GetAllAppointments()
        {
            var appointments = await _appointmentAppService.GetAllAppointmentsAsync();
            var response = new ApiResponse<IEnumerable<AppointmentResponseDto>>(true, "Appointments retrieved successfully", appointments, 200);
            return Ok(response);
        }

        /// <summary>
        /// Retrieves an appointment by its ID.
        /// </summary>
        /// <param name="id">The ID of the appointment.</param>
        /// <returns>The appointment DTO.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<AppointmentResponseDto>>> GetAppointmentById(int id)
        {
            var appointment = await _appointmentAppService.GetAppointmentByIdAsync(id);
            if (appointment == null)
            {
                var errorResponse = new ApiResponse<AppointmentResponseDto>(false, "Appointment not found", null, 404);
                return NotFound(errorResponse);
            }
            var response = new ApiResponse<AppointmentResponseDto>(true, "Appointment retrieved successfully", appointment, 200);
            return Ok(response);
        }

        /// <summary>
        /// Creates a new appointment.
        /// </summary>
        /// <param name="command">The command containing appointment details.</param>
        /// <returns>The ID of the newly created appointment.</returns>
        [HttpPost]
        public async Task<ActionResult<ApiResponse<int>>> CreateAppointment([FromBody] CreateAppointmentCommand command)
        {
            var jwtToken = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            var createdAppointmentId = await _appointmentAppService.CreateAppointmentAsync(command, jwtToken);
            var response = new ApiResponse<int>(true, "Appointment created successfully", createdAppointmentId, 201);
            return CreatedAtAction(nameof(GetAppointmentById), new { id = createdAppointmentId }, response);
        }

        /// <summary>
        /// Updates an existing appointment.
        /// </summary>
        /// <param name="id">The ID of the appointment to update.</param>
        /// <param name="command">The command containing updated appointment details.</param>
        /// <returns>Confirmation that the update was successful.</returns>
        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResponse<string>>> UpdateAppointment(int id, [FromBody] UpdateAppointmentCommand command)
        {
            if (id != command.Id)
            {
                var errorResponse = new ApiResponse<string>(false, "Appointment ID mismatch", null, 400);
                return BadRequest(errorResponse);
            }

            var jwtToken = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            await _appointmentAppService.UpdateAppointmentAsync(command, jwtToken);
            var response = new ApiResponse<string>(true, "Appointment updated successfully", null, 204);
            return Ok(response);
        }

        /// <summary>
        /// Deletes an appointment.
        /// </summary>
        /// <param name="id">The ID of the appointment to delete.</param>
        /// <returns>Confirmation that the deletion was successful.</returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse<string>>> DeleteAppointment(int id)
        {
            await _appointmentAppService.DeleteAppointmentAsync(new DeleteAppointmentCommand(id));
            var response = new ApiResponse<string>(true, "Appointment deleted successfully", null, 204);
            return Ok(response);
        }
    }
}