using Application.Models.ReponseDtos;
using MediatR;

namespace Application.Queries.Schedules
{
    /// <summary>
    /// Query to get all schedules.
    /// </summary>
    public class GetAllSchedulesQuery : IRequest<IEnumerable<ScheduleDto>>
    {
    }
}