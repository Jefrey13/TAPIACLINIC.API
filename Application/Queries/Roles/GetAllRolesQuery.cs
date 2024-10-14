using Application.Models;
using MediatR;

namespace Application.Queries.Roles
{
    /// <summary>
    /// Query to get all roles.
    /// </summary>
    public class GetAllRolesQuery : IRequest<IEnumerable<RoleDto>>
    {
    }
}