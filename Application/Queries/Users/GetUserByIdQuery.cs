using Application.Models;
using MediatR;

namespace Application.Queries.Users
{
    /// <summary>
    /// Query to get a user by their ID.
    /// </summary>
    public class GetUserByIdQuery : IRequest<UserDto>
    {
        public int Id { get; set; }

        public GetUserByIdQuery(int id)
        {
            Id = id;
        }
    }
}