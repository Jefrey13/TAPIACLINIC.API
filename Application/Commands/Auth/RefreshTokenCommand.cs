using Application.Models.ReponseDtos;
using Application.Models.RequestDtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.Auth
{
    public class RefreshTokenCommand : IRequest<LoginResponseDto>
    {
        public RefreshTokenRequestDto Request { get; set; }

        public RefreshTokenCommand(RefreshTokenRequestDto request)
        {
            Request = request;
        }
    }
}
