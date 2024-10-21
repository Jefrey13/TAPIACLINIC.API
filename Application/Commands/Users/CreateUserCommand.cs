using Application.Models.RequestDtos;
using MediatR;

namespace Application.Commands.Users
{
    /// <summary>
    /// Command to create a new user.
    /// </summary>
    public class CreateUserCommand : IRequest<int>
    {
        public UserRequestDto UserDto { get; set; }

        public CreateUserCommand(UserRequestDto userDto)
        {
            UserDto = userDto;
        }
    }
}