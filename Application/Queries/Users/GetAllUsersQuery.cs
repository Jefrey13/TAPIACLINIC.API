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
        public int? RoleId { get; set; }
        public int Id { get; set; }


        public GetAllUsersQuery(int? roleId, int id)
        {
            RoleId = roleId;
            Id = id;
        }
    }
}