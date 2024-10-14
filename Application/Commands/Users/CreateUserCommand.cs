using Application.Models;
using MediatR;

namespace Application.Commands.Users
{
    /// <summary>
    /// Command to create a new user.
    /// </summary>
    public class CreateUserCommand : IRequest<int>
    {
        public UserDto UserDto { get; set; }

        public CreateUserCommand(UserDto userDto)
        {
            UserDto = userDto;
        }
    }
}