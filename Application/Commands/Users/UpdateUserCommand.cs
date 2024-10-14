using Application.Models;
using MediatR;

namespace Application.Commands.Users
{
    /// <summary>
    /// Command to update an existing user.
    /// </summary>
    public class UpdateUserCommand : IRequest<Unit>
    {
        public int Id { get; set; }
        public UserDto UserDto { get; set; }

        public UpdateUserCommand(int id, UserDto userDto)
        {
            Id = id;
            UserDto = userDto;
        }
    }
}