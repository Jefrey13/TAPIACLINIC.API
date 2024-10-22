using Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.Auth
{
    public class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand, bool>
    {
        private readonly IUserRepository _userRepository;

        public ChangePasswordCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<bool> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUserByUserNameAsync(request.RequestDto.UserName);

            if (user == null || !BCrypt.Net.BCrypt.Verify(request.RequestDto.CurrentPassword, user.Password))
            {
                throw new UnauthorizedAccessException("Invalid username or current password.");
            }

            // Encriptar la nueva contraseña y actualizar el usuario
            user.Password = BCrypt.Net.BCrypt.HashPassword(request.RequestDto.NewPassword);
            await _userRepository.UpdatePasswordAsync(user);

            return true;
        }
    }
}
