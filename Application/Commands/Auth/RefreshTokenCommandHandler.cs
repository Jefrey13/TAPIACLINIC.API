using Application.Models.ReponseDtos;
using Application.Services;
using Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.Auth
{
    public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, LoginResponseDto>
    {
        private readonly ITokenRepository _tokenRepository;
        private readonly IJwtTokenService _jwtTokenService;

        public RefreshTokenCommandHandler(ITokenRepository tokenRepository, IJwtTokenService jwtTokenService)
        {
            _tokenRepository = tokenRepository;
            _jwtTokenService = jwtTokenService;
        }

        public async Task<LoginResponseDto> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            var storedToken = await _tokenRepository.GetTokenByValueAsync(request.Request.RefreshToken);

            if (storedToken == null || storedToken.ExpirationDate < DateTime.UtcNow)
            {
                throw new UnauthorizedAccessException("Invalid or expired refresh token");
            }

            var newAccessToken = _jwtTokenService.GenerateAccessToken(storedToken.User);

            return new LoginResponseDto
            {
                AccessToken = newAccessToken,
                RefreshToken = request.Request.RefreshToken
            };
        }
    }
}