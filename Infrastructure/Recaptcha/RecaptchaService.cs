using Application.Services;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Recaptcha
{
    public class RecaptchaService : IRecaptchaService
    {
        private readonly RecaptchaSettings _recaptchaSettings;

        public RecaptchaService(IOptions<RecaptchaSettings> recaptchaSettings)
        {
            _recaptchaSettings = recaptchaSettings.Value;
        }

        public async Task<bool> VerifyRecaptchaAsync(string recaptchaToken)
        {
            using (var client = new HttpClient())
            {
                var content = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("secret", _recaptchaSettings.SecretKey),
                    new KeyValuePair<string, string>("response", recaptchaToken)
                });

                var response = await client.PostAsync("https://www.google.com/recaptcha/api/siteverify", content);
                var responseString = await response.Content.ReadAsStringAsync();
                var recaptchaResponse = JsonConvert.DeserializeObject<RecaptchaResponse>(responseString);

                return recaptchaResponse?.Success ?? false;
            }
        }
    }
}
