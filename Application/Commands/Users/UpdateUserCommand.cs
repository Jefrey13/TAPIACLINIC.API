using Application.Models.RequestDtos;
using Application.Models.RequestDtos.UpdateRequestDto;
using MediatR;

namespace Application.Commands.Users
{
    /// <summary>
    /// Command to update an existing user.
    /// </summary>
    public class UpdateUserCommand : IRequest<bool>
    {
        public int Id { get; set; }
        public UserUpdateRequestDto UserDto { get; set; }

        public UpdateUserCommand(int id, UserUpdateRequestDto userDto)
        {
            Id = id;
            UserDto = userDto;
        }
    }
}