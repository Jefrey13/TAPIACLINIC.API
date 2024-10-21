using Application.Models.ReponseDtos;
using MediatR;

namespace Application.Queries.Staffs
{
    /// <summary>
    /// Query to get a staff member by User ID.
    /// </summary>
    public class GetStaffByUserIdQuery : IRequest<StaffResponseDto>
    {
        public int UserId { get; set; }

        public GetStaffByUserIdQuery(int userId)
        {
            UserId = userId;
        }
    }
}