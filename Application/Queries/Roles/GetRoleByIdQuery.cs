using Application.Models.ReponseDtos;
using MediatR;

namespace Application.Queries.Roles
{
    /// <summary>
    /// Query to get a role by its ID.
    /// </summary>
    public class GetRoleByIdQuery : IRequest<RoleResponseDto>
    {
        public int Id { get; set; }

        public GetRoleByIdQuery(int id)
        {
            Id = id;
        }
    }
}