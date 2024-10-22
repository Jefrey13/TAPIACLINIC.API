using Application.Models.ReponseDtos;
using MediatR;

namespace Application.Queries.Schedules
{
    /// <summary>
    /// Query to retrieve a Schedule by its ID.
    /// </summary>
    public class GetScheduleByIdQuery : IRequest<ScheduleResponseDto>
    {
        public int Id { get; }

        public GetScheduleByIdQuery(int id)
        {
            Id = id;
        }
    }
}