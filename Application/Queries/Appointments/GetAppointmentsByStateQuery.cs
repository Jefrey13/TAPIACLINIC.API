using Application.Models.ReponseDtos;
using Application.Models.ResponseDtos;
using MediatR;
using System.Collections.Generic;

namespace Application.Queries.Appointments
{
    public class GetAppointmentsByStateQuery : IRequest<IEnumerable<AppointmentResponseDto>>
    {
        public string StateName { get; set; }

        public GetAppointmentsByStateQuery(string stateName)
        {
            StateName = stateName;
        }
    }
}