using Application.Commands.Auth;
using Application.Models.ReponseDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public interface IAuthService
    {
        Task<LoginResponseDto> LoginAsync(LoginCommand command);

        Task<LoginResponseDto> RefreshTokenAsync(RefreshTokenCommand command);

        bool VerifyPassword(string inputPassword, string storedHash);
    }
}
