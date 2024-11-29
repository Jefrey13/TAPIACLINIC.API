using Application.Commands.Appointments;
using Application.Models.RequestDtos;
using Application.Models.ResponseDtos;
using Application.Services;
using API.Utils;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Queries.Appointments;
using Application.Services.Impl;

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

            if (appointments == null || !appointments.Any())
            {
                return NotFound(new ApiResponse<IEnumerable<AppointmentResponseDto>>(false, "No appointments found", null, 404));
            }

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
            if (id <= 0)
            {
                return BadRequest(new ApiResponse<AppointmentResponseDto>(false, "Invalid appointment ID", null, 400));
            }

            var appointment = await _appointmentAppService.GetAppointmentByIdAsync(id);
            if (appointment == null)
            {
                return NotFound(new ApiResponse<AppointmentResponseDto>(false, "Appointment not found", null, 404));
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
        public async Task<ActionResult<ApiResponse<int?>>> CreateAppointment([FromBody] CreateAppointmentCommand command)
        {
            if (command == null)
            {
                return BadRequest(new ApiResponse<int?>(false, "Appointment data is required", null, 400));
            }

            var jwtToken = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            var createdAppointmentId = await _appointmentAppService.CreateAppointmentAsync(command, jwtToken);
            var response = new ApiResponse<int?>(true, "Appointment created successfully", createdAppointmentId, 201);

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
            if (id <= 0 || command == null)
            {
                return BadRequest(new ApiResponse<string>(false, "Invalid appointment ID or data", null, 400));
            }

            var jwtToken = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            await _appointmentAppService.UpdateAppointmentAsync(command, jwtToken);
            var response = new ApiResponse<string>(true, "Appointment updated successfully", null, 200);
            return Ok(response);
        }

        /// <summary>
        /// Deletes an appointment.
        /// </summary>
        /// <param name="id">The ID of the appointment to delete.</param>
        /// <returns>Confirmation that the deletion was successful.</returns>
        [HttpPatch("{id}")]
        public async Task<ActionResult<ApiResponse<string>>> DeleteAppointment(int id)
        {
            if (id <= 0)
            {
                return BadRequest(new ApiResponse<string>(false, "Invalid appointment ID", null, 400));
            }

            var appointment = await _appointmentAppService.GetAppointmentByIdAsync(id);
            if (appointment == null)
            {
                return NotFound(new ApiResponse<string>(false, "Appointment not found", null, 404));
            }

            await _appointmentAppService.DeleteAppointmentAsync(new DeleteAppointmentCommand(id));
            var response = new ApiResponse<string>(true, "Appointment deleted successfully", null, 204);
            return Ok(response);
        }

        /// <summary>
        /// Retrieves appointments by state name.
        /// </summary>
        /// <param name="stateName">The name of the state (e.g., "Scheduled", "Completed").</param>
        /// <returns>A list of appointments in the specified state.</returns>
        [HttpGet("by-state/{stateName}")]
        public async Task<ActionResult<ApiResponse<IEnumerable<AppointmentResponseDto>>>> GetAppointmentsByState(string stateName)
        {
            if (string.IsNullOrWhiteSpace(stateName))
            {
                return BadRequest(new ApiResponse<IEnumerable<AppointmentResponseDto>>(false, "Invalid state name", null, 400));
            }

            var appointments = await _appointmentAppService.GetAppointmentsByStateAsync(stateName);

            if (appointments == null || !appointments.Any())
            {
                return NotFound(new ApiResponse<IEnumerable<AppointmentResponseDto>>(false, "No appointments found for the specified state", null, 404));
            }

            var response = new ApiResponse<IEnumerable<AppointmentResponseDto>>(true, "Appointments retrieved by state successfully", appointments, 200);
            return Ok(response);
        }

        /// <summary>
        /// Updates the state of an appointment.
        /// </summary>
        /// <param name="id">The ID of the appointment to update.</param>
        /// <param name="stateName">The new state name for the appointment (e.g., "Scheduled", "Completed").</param>
        /// <returns>Confirmation that the state was successfully updated.</returns>
        [HttpPatch("{id}/state")]
        public async Task<ActionResult<ApiResponse<string>>> UpdateAppointmentState(int id, [FromBody] string stateName)
        {
            if (id <= 0 || string.IsNullOrWhiteSpace(stateName))
            {
                return BadRequest(new ApiResponse<string>(false, "Invalid appointment ID or state name", null, 400));
            }

            var jwtToken = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            await _appointmentAppService.UpdateAppointmentStateAsync(
                new UpdateAppointmentStateCommand(id, stateName), jwtToken);

            var response = new ApiResponse<string>(true, "Appointment state updated successfully", null, 200);
            return Ok(response);
        }
    }
}