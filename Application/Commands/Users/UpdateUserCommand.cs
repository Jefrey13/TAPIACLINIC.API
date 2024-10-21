using Application.Models.RequestDtos;
using MediatR;

namespace Application.Commands.Users
{
    /// <summary>
    /// Command to update an existing user.
    /// </summary>
    public class UpdateUserCommand : IRequest<Unit>
    {
        public int Id { get; set; }
        public UserRequestDto UserDto { get; set; }

        public UpdateUserCommand(int id, UserRequestDto userDto)
        {
            Id = id;
            UserDto = userDto;
        }
    }
}