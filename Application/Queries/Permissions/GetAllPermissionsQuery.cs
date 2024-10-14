using Application.Models;
using MediatR;

namespace Application.Queries.Permissions
{
    /// <summary>
    /// Query to get all permissions.
    /// </summary>
    public class GetAllPermissionsQuery : IRequest<IEnumerable<PermissionDto>>
    {
    }
}