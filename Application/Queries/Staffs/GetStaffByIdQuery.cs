using Application.Models;
using MediatR;

namespace Application.Queries.Staffs
{
    /// <summary>
    /// Query to get a staff member by ID.
    /// </summary>
    public class GetStaffByIdQuery : IRequest<StaffDto>
    {
        public int Id { get; set; }

        public GetStaffByIdQuery(int id)
        {
            Id = id;
        }
    }
}