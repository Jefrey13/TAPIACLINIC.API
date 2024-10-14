using Application.Models;
using MediatR;

namespace Application.Queries.Schedules
{
    /// <summary>
    /// Query to get a schedule by its ID.
    /// </summary>
    public class GetScheduleByIdQuery : IRequest<ScheduleDto>
    {
        public int Id { get; set; }

        public GetScheduleByIdQuery(int id)
        {
            Id = id;
        }
    }
}