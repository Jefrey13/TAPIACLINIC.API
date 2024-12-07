using Application.Commands.Users;
using Application.Services;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Handlers.Users
{
    public class VerifyRecaptchaCommandHandler : IRequestHandler<VerifyRecaptchaCommand, bool>
    {
        private readonly IRecaptchaService _recaptchaService;

        public VerifyRecaptchaCommandHandler(IRecaptchaService recaptchaService)
        {
            _recaptchaService = recaptchaService;
        }

        public async Task<bool> Handle(VerifyRecaptchaCommand request, CancellationToken cancellationToken)
        {
            return await _recaptchaService.VerifyRecaptchaAsync(request.RecaptchaToken);
        }
    }
}