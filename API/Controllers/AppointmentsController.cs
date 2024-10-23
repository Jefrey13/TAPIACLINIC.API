using Application.Commands.Appointments;
using Application.Models.RequestDtos;
using Application.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

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
        public async Task<ActionResult<IEnumerable<AppointmentRequestDto>>> GetAllAppointments()
        {
            var appointments = await _appointmentAppService.GetAllAppointmentsAsync();
            return Ok(appointments);
        }

        /// <summary>
        /// Retrieves an appointment by its ID.
        /// </summary>
        /// <param name="id">The ID of the appointment.</param>
        /// <returns>The appointment DTO.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<AppointmentRequestDto>> GetAppointmentById(int id)
        {
            var appointment = await _appointmentAppService.GetAppointmentByIdAsync(id);
            if (appointment == null)
            {
                return NotFound();
            }
            return Ok(appointment);
        }

        /// <summary>
        /// Creates a new appointment.
        /// </summary>
        /// <param name="command">The command containing appointment details.</param>
        /// <returns>The ID of the newly created appointment.</returns>
        [HttpPost]
        public async Task<ActionResult<int>> CreateAppointment([FromBody] CreateAppointmentCommand command)
        {
            var jwtToken = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            var createdAppointmentId = await _appointmentAppService.CreateAppointmentAsync(command, jwtToken);
            return CreatedAtAction(nameof(GetAppointmentById), new { id = createdAppointmentId }, createdAppointmentId);
        }

        /// <summary>
        /// Updates an existing appointment.
        /// </summary>
        /// <param name="id">The ID of the appointment to update.</param>
        /// <param name="command">The command containing updated appointment details.</param>
        /// <returns>No content if the update was successful.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAppointment(int id, [FromBody] UpdateAppointmentCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest("Appointment ID mismatch.");
            }

            var jwtToken = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            await _appointmentAppService.UpdateAppointmentAsync(command, jwtToken);
            return NoContent();
        }

        /// <summary>
        /// Deletes an appointment.
        /// </summary>
        /// <param name="id">The ID of the appointment to delete.</param>
        /// <returns>No content if the deletion was successful.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAppointment(int id)
        {
            await _appointmentAppService.DeleteAppointmentAsync(new DeleteAppointmentCommand(id));
            return NoContent();
        }
    }
}