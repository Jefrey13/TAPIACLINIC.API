using Application.Models.ReponseDtos;
using MediatR;

namespace Application.Queries.Schedules
{
    /// <summary>
    /// Query to retrieve all schedules.
    /// </summary>
    public class GetAllSchedulesQuery : IRequest<IEnumerable<ScheduleResponseDto>>
    {
    }
}