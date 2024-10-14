using Application.Models;
using MediatR;
using System.Collections.Generic;

namespace Application.Queries.Users
{
    /// <summary>
    /// Query to get all users.
    /// </summary>
    public class GetAllUsersQuery : IRequest<IEnumerable<UserDto>>
    {
    }
}