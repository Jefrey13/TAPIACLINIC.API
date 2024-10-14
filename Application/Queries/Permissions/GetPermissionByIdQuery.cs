using Application.Models;
using MediatR;

namespace Application.Queries.Permissions
{
    /// <summary>
    /// Query to get a permission by its ID.
    /// </summary>
    public class GetPermissionByIdQuery : IRequest<PermissionDto>
    {
        public int Id { get; set; }

        public GetPermissionByIdQuery(int id)
        {
            Id = id;
        }
    }
}