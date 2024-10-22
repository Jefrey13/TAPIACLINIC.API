using Application.Models.ReponseDtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.Schedules
{
    /// <summary>
    /// Query to retrieve all schedules for a specific specialty.
    /// </summary>
    public class GetSchedulesBySpecialtyQuery : IRequest<IEnumerable<ScheduleResponseDto>>
    {
        public int SpecialtyId { get; }

        public GetSchedulesBySpecialtyQuery(int specialtyId)
        {
            SpecialtyId = specialtyId;
        }
    }
}
