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
        public int? RoleId { get; set; }
        public int Id { get; set; }


        public GetAllAppointmentsQuery(int? roleId, int id)
        {
            RoleId = roleId;
            Id = id;
        }
    }
}