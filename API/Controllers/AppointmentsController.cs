using Application.Commands.Appointments;
using Application.Models;
using Application.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentsController : ControllerBase
    {
        private readonly IAppointmentAppService _appointmentAppService;

        public AppointmentsController(IAppointmentAppService appointmentAppService)
        {
            _appointmentAppService = appointmentAppService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AppointmentDto>>> GetAllAppointments()
        {
            var appointments = await _appointmentAppService.GetAllAppointmentsAsync();
            return Ok(appointments);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AppointmentDto>> GetAppointmentById(int id)
        {
            var appointment = await _appointmentAppService.GetAppointmentByIdAsync(id);
            if (appointment == null)
            {
                return NotFound();
            }
            return Ok(appointment);
        }

        [HttpPost]
        public async Task<ActionResult<int>> CreateAppointment([FromBody] CreateAppointmentCommand command)
        {
            var createdAppointmentId = await _appointmentAppService.CreateAppointmentAsync(command);
            return CreatedAtAction(nameof(GetAppointmentById), new { id = createdAppointmentId }, createdAppointmentId);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAppointment(int id, [FromBody] UpdateAppointmentCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest("Appointment ID mismatch.");
            }

            await _appointmentAppService.UpdateAppointmentAsync(command);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAppointment(int id)
        {
            await _appointmentAppService.DeleteAppointmentAsync(new DeleteAppointmentCommand(id));
            return NoContent();
        }
    }
}