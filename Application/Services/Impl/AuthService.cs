using Application.Commands.Auth;
using Application.Models.ReponseDtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Impl
{
    public class AuthService : IAuthService
    {
        private readonly IMediator _mediator;

        public AuthService(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<LoginResponseDto> LoginAsync(LoginCommand command)
        {
            return await _mediator.Send(command);
        }

        public async Task<LoginResponseDto> RefreshTokenAsync(RefreshTokenCommand command)
        {
            return await _mediator.Send(command);
        }
        public bool VerifyPassword(string inputPassword, string storedHash)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashedPassword = Convert.ToBase64String(sha256.ComputeHash(Encoding.UTF8.GetBytes(inputPassword)));
                return hashedPassword == storedHash;
            }
        }
    }
}
