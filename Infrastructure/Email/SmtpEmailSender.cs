using Application.Services;
using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Infrastructure.Email
{
    public class SmtpEmailSender : IEmailSender
    {
        private readonly IConfiguration _configuration;

        public SmtpEmailSender(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendEmailAsync(string to, string subject, string body)
        {
            // Create an SMTP client using the configuration settings
            var smtpClient = new SmtpClient(_configuration["SmtpSettings:Server"])
            {
                Port = int.Parse(_configuration["SmtpSettings:Port"]),
                Credentials = new NetworkCredential(
                    _configuration["SmtpSettings:SenderEmail"],
                    _configuration["SmtpSettings:SenderPassword"]
                ),
                EnableSsl = true  // Use SSL to send secure emails
            };

            // Create a mail message object
            var mailMessage = new MailMessage
            {
                From = new MailAddress(_configuration["SmtpSettings:SenderEmail"], _configuration["SmtpSettings:SenderName"]),
                Subject = subject,
                Body = body,
                IsBodyHtml = true  // Enable HTML content in the email body
            };

            // Add recipient to the email
            mailMessage.To.Add(to);

            // Send the email asynchronously
            await smtpClient.SendMailAsync(mailMessage);
        }
    }
}