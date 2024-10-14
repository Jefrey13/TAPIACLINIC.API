using Application.Models;
using MediatR;

namespace Application.Queries.Staffs
{
    /// <summary>
    /// Query to get all staff members.
    /// </summary>
    public class GetAllStaffsQuery : IRequest<IEnumerable<StaffDto>>
    {
    }
}