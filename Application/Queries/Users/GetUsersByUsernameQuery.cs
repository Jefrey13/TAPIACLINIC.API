using Application.Models.ReponseDtos;
using MediatR;

namespace Application.Queries.Users
{
    /// <summary>
    /// Query to retrieve users by username.
    /// </summary>
    public class GetUsersByUsernameQuery : IRequest<UserResponseDto>
    {
        public string Username { get; set; }

        public GetUsersByUsernameQuery(string username)
        {
            Username = username;
        }
    }
}