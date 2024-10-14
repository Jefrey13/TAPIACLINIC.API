using Application.Models;
using MediatR;

namespace Application.Queries.Appointments
{
    /// <summary>
    /// Query to get an appointment by its ID.
    /// </summary>
    public class GetAppointmentByIdQuery : IRequest<AppointmentDto>
    {
        public int Id { get; set; }

        public GetAppointmentByIdQuery(int id)
        {
            Id = id;
        }
    }
}