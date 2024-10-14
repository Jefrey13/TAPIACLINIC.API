using Application.Models;
using MediatR;
using System.Collections.Generic;

namespace Application.Queries.Appointments
{
    /// <summary>
    /// Query to get all appointments.
    /// </summary>
    public class GetAllAppointmentsQuery : IRequest<IEnumerable<AppointmentDto>>
    {
    }
}