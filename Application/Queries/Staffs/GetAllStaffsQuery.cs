using Application.Models.ReponseDtos;
using MediatR;
using System.Collections.Generic;

namespace Application.Queries.Staffs
{
    /// <summary>
    /// Query to get all staff members.
    /// </summary>
    public class GetAllStaffsQuery : IRequest<IEnumerable<StaffResponseDto>>
    {
    }
}