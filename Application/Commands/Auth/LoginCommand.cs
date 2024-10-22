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
    public class LoginCommand : IRequest<LoginResponseDto>
    {
        public LoginRequestDto Request { get; set; }

        public LoginCommand(LoginRequestDto request)
        {
            Request = request;
        }
    }
}
