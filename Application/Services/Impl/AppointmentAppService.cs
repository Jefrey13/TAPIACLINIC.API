using Application.Commands.Appointments;
using Application.Models.RequestDtos;
using Application.Models.ResponseDtos;
using Application.Queries.Appointments;
using Application.Services;
using Domain.Repositories;
using MediatR;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Services.Impl
{
    /// <summary>
    /// Service implementation for managing appointments.
    /// </summary>
    public class AppointmentAppService : IAppointmentAppService
    {
        private readonly IMediator _mediator;
        private readonly IEmailSender _emailSender;
        private readonly IJwtTokenService _jwtTokenService;
        private readonly IUserRepository _userRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="AppointmentAppService"/> class.
        /// </summary>
        /// <param name="mediator">The mediator for handling commands and queries.</param>
        /// <param name="emailSender">The service for sending emails.</param>
        /// <param name="jwtTokenService">The service for handling JWT tokens.</param>
        /// <param name="userRepository">The repository for user-related data.</param>
        public AppointmentAppService(IMediator mediator, IEmailSender emailSender, IJwtTokenService jwtTokenService, IUserRepository userRepository)
        {
            _mediator = mediator;
            _emailSender = emailSender;
            _jwtTokenService = jwtTokenService;
            _userRepository = userRepository;
        }

        /// <inheritdoc />
        public async Task<int> CreateAppointmentAsync(CreateAppointmentCommand command, string jwtToken)
        {
            // Extract the username from the JWT token
            var username = _jwtTokenService.GetUsernameFromToken(jwtToken);
            var user = await _userRepository.GetUserByUserNameAsync(username);

            if (user == null)
            {
                throw new Exception("User not found.");
            }

            // Create the appointment
            var appointmentId = await _mediator.Send(command);
            var appointment = await _mediator.Send(new GetAppointmentByIdQuery(appointmentId));

            // Send notification email
            var emailBody = $@"
                <h1>Appointment Created</h1>
                <p>State: {appointment.State.StateName}</p>
                <p>Day: {appointment.Schedule.DayOfWeek}</p>
                <p>Start Time: {appointment.Schedule.StartTime}</p>
                <p>End Time: {appointment.Schedule.EndTime}</p>";

            await _emailSender.SendEmailAsync(user.Email.Value.ToString(), "Appointment Created", emailBody);

            return appointmentId;
        }

        /// <inheritdoc />
        public async Task UpdateAppointmentAsync(UpdateAppointmentCommand command, string jwtToken)
        {
            // Extract the username from the JWT token
            var username = _jwtTokenService.GetUsernameFromToken(jwtToken);
            var user = await _userRepository.GetUserByUserNameAsync(username);

            if (user == null)
            {
                throw new Exception("User not found.");
            }

            // Update the appointment
            await _mediator.Send(command);
            var appointment = await _mediator.Send(new GetAppointmentByIdQuery(command.Id));

            // Send notification email
            var emailBody = $@"
                <h1>Notificación de cita</h1>
                <p>Estado: {appointment.State.StateName}</p>
                <p>Dia: {appointment.Schedule.DayOfWeek}</p>
                <p>Hora de Inicio: {appointment.Schedule.StartTime}</p>
                <p>Hora de finalización: {appointment.Schedule.EndTime}</p>";

            await _emailSender.SendEmailAsync(user.Email.Value.ToString(), "Appointment Updated", emailBody);
        }
        public async Task DeleteAppointmentAsync(DeleteAppointmentCommand command)
        {
            await _mediator.Send(command);
        }

        public async Task<IEnumerable<AppointmentResponseDto>> GetAllAppointmentsAsync()
        {
            return await _mediator.Send(new GetAllAppointmentsQuery());
        }

        public async Task<AppointmentResponseDto> GetAppointmentByIdAsync(int id)
        {
            return await _mediator.Send(new GetAppointmentByIdQuery(id));
        }

        public async Task<IEnumerable<AppointmentResponseDto>> GetAppointmentsByStateAsync(string stateName)
        {
            return await _mediator.Send(new GetAppointmentsByStateQuery(stateName));
        }
    }
}