using Application.Models.RequestDtos;
using Application.Models.ResponseDtos;
using MediatR;
using System.Collections.Generic;

namespace Application.Queries.Appointments
{
    /// <summary>
    /// Query to get all appointments.
    /// </summary>
    public class GetAllAppointmentsQuery : IRequest<IEnumerable<AppointmentResponseDto>>
    {
    }
}