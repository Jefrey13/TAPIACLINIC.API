using Application.Models.ReponseDtos;
using MediatR;
using System.Collections.Generic;

namespace Application.Queries.Users
{
    /// <summary>
    /// Query to retrieve all users.
    /// </summary>
    public class GetAllUsersQuery : IRequest<IEnumerable<UserResponseDto>>
    {
    }
}