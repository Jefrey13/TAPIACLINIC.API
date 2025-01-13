using Application.Commands.Appointments;
using Application.Models.RequestDtos;
using Application.Models.ResponseDtos;
using Application.Services;
using API.Utils;
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
        [HttpGet]
        public async Task<ActionResult<ApiResponse<IEnumerable<AppointmentResponseDto>>>> GetAllAppointments()
        {
            try
            {
                var jwtToken = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
                var appointments = await _appointmentAppService.GetAllAppointmentsAsync(jwtToken);

                if (appointments == null || !appointments.Any())
                {
                    return ResponseHelper.NotFound<IEnumerable<AppointmentResponseDto>>("No appointments found");
                }

                return ResponseHelper.Success(appointments, "Appointments retrieved successfully");
            }
            catch (Exception ex)
            {
                return ResponseHelper.Error<IEnumerable<AppointmentResponseDto>>($"An error occurred: {ex.Message}");
            }
        }

        /// <summary>
        /// Retrieves an appointment by its ID.
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<AppointmentResponseDto>>> GetAppointmentById(int id)
        {
            if (id <= 0)
            {
                return ResponseHelper.BadRequest<AppointmentResponseDto>("Invalid appointment ID");
            }

            try
            {
                var appointment = await _appointmentAppService.GetAppointmentByIdAsync(id);
                if (appointment == null)
                {
                    return ResponseHelper.NotFound<AppointmentResponseDto>("Appointment not found");
                }

                return ResponseHelper.Success(appointment, "Appointment retrieved successfully");
            }
            catch (Exception ex)
            {
                return ResponseHelper.Error<AppointmentResponseDto>($"An error occurred: {ex.Message}");
            }
        }

        /// <summary>
        /// Creates a new appointment.
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<ApiResponse<int>>> CreateAppointment([FromBody] AppointmentRequestDto appointmentRequestDto)
        {
            if (appointmentRequestDto == null)
            {
                return ResponseHelper.BadRequest<int>("Appointment data is required");
            }

            try
            {
                var jwtToken = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
                var appointmentId = await _appointmentAppService.CreateAppointmentAsync(new CreateAppointmentCommand(appointmentRequestDto), jwtToken);

                return ResponseHelper.Success(appointmentId, "Appointment created successfully", 201);
            }
            catch (Exception ex)
            {
                return ResponseHelper.Error<int>($"An error occurred: {ex.Message}", 500);
            }
        }

        /// <summary>
        /// Updates an existing appointment.
        /// </summary>
        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResponse<string>>> UpdateAppointment(int id, [FromBody] AppointmentRequestDto appointmentRequestDto)
        {
            if (id <= 0 || appointmentRequestDto == null)
            {
                return ResponseHelper.BadRequest<string>("Invalid appointment ID or data");
            }

            try
            {
                var jwtToken = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
                await _appointmentAppService.UpdateAppointmentAsync(new UpdateAppointmentCommand(id, appointmentRequestDto), jwtToken);

                return ResponseHelper.Success<string>(null, "Appointment updated successfully");
            }
            catch (Exception ex)
            {
                return ResponseHelper.Error<string>($"An error occurred: {ex.Message}");
            }
        }

        /// <summary>
        /// Deletes an appointment by ID.
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse<string>>> DeleteAppointment(int id)
        {
            if (id <= 0)
            {
                return ResponseHelper.BadRequest<string>("Invalid appointment ID");
            }

            try
            {
                await _appointmentAppService.DeleteAppointmentAsync(new DeleteAppointmentCommand(id));
                return ResponseHelper.Success<string>(null, "Appointment deleted successfully");
            }
            catch (Exception ex)
            {
                return ResponseHelper.Error<string>($"An error occurred: {ex.Message}");
            }
        }

        /// <summary>
        /// Retrieves appointments by state name.
        /// </summary>
        [HttpGet("by-state/{stateName}")]
        public async Task<ActionResult<ApiResponse<IEnumerable<AppointmentResponseDto>>>> GetAppointmentsByState(string stateName)
        {
            if (string.IsNullOrWhiteSpace(stateName))
            {
                return ResponseHelper.BadRequest<IEnumerable<AppointmentResponseDto>>("Invalid state name");
            }

            try
            {
                var appointments = await _appointmentAppService.GetAppointmentsByStateAsync(stateName);

                if (appointments == null || !appointments.Any())
                {
                    return ResponseHelper.NotFound<IEnumerable<AppointmentResponseDto>>("No appointments found for the specified state");
                }

                return ResponseHelper.Success(appointments, "Appointments retrieved successfully");
            }
            catch (Exception ex)
            {
                return ResponseHelper.Error<IEnumerable<AppointmentResponseDto>>($"An error occurred: {ex.Message}");
            }
        }

        /// <summary>
        /// Updates the state of an appointment.
        /// </summary>
        [HttpPatch("{id}/state")]
        public async Task<ActionResult<ApiResponse<string>>> UpdateAppointmentState(int id, [FromBody] string stateName)
        {
            if (id <= 0 || string.IsNullOrWhiteSpace(stateName))
            {
                return ResponseHelper.BadRequest<string>("Invalid appointment ID or state name");
            }

            try
            {
                var jwtToken = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
                await _appointmentAppService.UpdateAppointmentStateAsync(new UpdateAppointmentStateCommand(id, stateName), jwtToken);

                return ResponseHelper.Success<string>(null, "Appointment state updated successfully");
            }
            catch (Exception ex)
            {
                return ResponseHelper.Error<string>($"An error occurred: {ex.Message}");
            }
        }
    }
}