using Application.Models.RequestDtos;
using Application.Models.ResponseDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public interface IContactService
    {
        Task<ServiceResult> SendMessageAsync(ContactRequestDto contactRequestDto, string recaptchaToken);
    }
}
