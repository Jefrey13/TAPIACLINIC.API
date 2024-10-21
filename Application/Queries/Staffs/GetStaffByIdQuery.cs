using Application.Models.ReponseDtos;
using MediatR;

namespace Application.Queries.Staffs
{
    /// <summary>
    /// Query to get a staff member by ID.
    /// </summary>
    public class GetStaffByIdQuery : IRequest<StaffResponseDto>
    {
        public int Id { get; set; }

        public GetStaffByIdQuery(int id)
        {
            Id = id;
        }
    }
}