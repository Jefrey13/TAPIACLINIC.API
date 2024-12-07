using MediatR;

namespace Application.Commands.Users
{
    public class VerifyRecaptchaCommand : IRequest<bool>
    {
        public string RecaptchaToken { get; set; }

        public VerifyRecaptchaCommand(string recaptchaToken)
        {
            RecaptchaToken = recaptchaToken;
        }
    }
}