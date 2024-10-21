using Application.Models.ReponseDtos;
using MediatR;

namespace Application.Queries.Users
{
    /// <summary>
    /// Query to retrieve a user by their ID.
    /// </summary>
    public class GetUserByIdQuery : IRequest<UserResponseDto>
    {
        public int Id { get; set; }

        public GetUserByIdQuery(int id)
        {
            Id = id;
        }
    }
}