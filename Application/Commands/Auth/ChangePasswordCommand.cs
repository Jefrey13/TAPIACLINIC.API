using Application.Models.RequestDtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.Auth
{
    public class ChangePasswordCommand : IRequest<bool>
    {
        public ChangePasswordRequestDto RequestDto { get; set; }

        public ChangePasswordCommand(ChangePasswordRequestDto requestDto)
        {
            RequestDto = requestDto;
        }
    }
}
