using Application.Services;
using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Infrastructure.Email
{
    /// <summary>
    /// SmtpEmailSender is responsible for sending emails using the SMTP protocol.
    /// </summary>
    public class SmtpEmailSender : IEmailSender
    {
        private readonly IConfiguration _configuration;

        /// <summary>
        /// Initializes a new instance of the <see cref="SmtpEmailSender"/> class.
        /// </summary>
        /// <param name="configuration">Application configuration to fetch SMTP settings.</param>
        public SmtpEmailSender(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// Asynchronously sends an email using the specified recipient, subject, and body.
        /// </summary>
        /// <param name="to">Recipient email address.</param>
        /// <param name="subject">Subject of the email.</param>
        /// <param name="body">Body content of the email.</param>
        /// <returns>A task that represents the asynchronous operation of sending an email.</returns>
        public async Task SendEmailAsync(string to, string subject, string body)
        {
            try
            {
                // Trim any leading or trailing spaces from the recipient email address
                to = to.Trim();

                // Create and configure the SMTP client
                var smtpClient = new SmtpClient(_configuration["EmailSettings:SmtpServer"])
                {
                    Port = int.Parse(_configuration["EmailSettings:Port"]),
                    Credentials = new NetworkCredential(_configuration["EmailSettings:Username"], _configuration["EmailSettings:Password"]),
                    EnableSsl = true,
                };

                // Wrap the plain text body content into basic HTML structure
                string htmlBody = $"<html><body><p>{body}</p></body></html>";

                // Construct the email message
                var mailMessage = new MailMessage
                {
                    From = new MailAddress(_configuration["EmailSettings:From"], "Clínica Tapia"),  // The sender's email address and name
                    Subject = subject,  // Subject of the email
                    Body = htmlBody,  // The body content wrapped in HTML
                    IsBodyHtml = true,  // Indicates that the body content should be treated as HTML
                };

                // Add the recipient to the email message
                mailMessage.To.Add(to);

                // Asynchronously send the email
                await smtpClient.SendMailAsync(mailMessage);

            }
            catch (SmtpException smtpEx)
            {
                throw new Exception($"SMTP error: {smtpEx.StatusCode} - {smtpEx.Message}", smtpEx);
            }
            catch (Exception ex)
            {
                throw new Exception($"Ocurrió al envir el correo: {ex.Message}");
            }
        }


        /// <summary>
        /// Sends an email on behalf of the specified sender to the configured recipient.
        /// </summary>
        /// <param name="from">The email address of the sender.</param>
        /// <param name="subject">The subject of the email.</param>
        /// <param name="body">The body content of the email.</param>
        /// <returns>A task representing the asynchronous operation of sending the email.</returns>
        public async Task SendEmailFromUserAsync(string from, string subject, string body)
        {
            try
            {
                // Validate the sender email
                if (string.IsNullOrWhiteSpace(from))
                {
                    throw new ArgumentException("El correo del remitente no puede estar vacío.", nameof(from));
                }

                // Trim leading or trailing spaces from the sender's email address
                from = from.Trim();

                // Create and configure the SMTP client
                var smtpClient = new SmtpClient(_configuration["EmailSettings:SmtpServer"])
                {
                    Port = int.Parse(_configuration["EmailSettings:Port"]),
                    Credentials = new NetworkCredential(
                        _configuration["EmailSettings:Username"],
                        _configuration["EmailSettings:Password"]
                    ),
                    EnableSsl = true,
                };

                // Wrap the plain text body content into a basic HTML structure
                string htmlBody = $"<html><body><p>{body}</p></body></html>";

                // Construct the email message
                var mailMessage = new MailMessage
                {
                    From = new MailAddress(from, "Con"),  // Sender's email address and name
                    Subject = subject,                       // Email subject
                    Body = htmlBody,                         // Email body wrapped in HTML
                    IsBodyHtml = true,                       // Indicates that the body is in HTML format
                };

                // Add the configured recipient as the recipient of the email
                mailMessage.To.Add(_configuration["EmailSettings:From"]);

                // Asynchronously send the email
                await smtpClient.SendMailAsync(mailMessage);
            }
            catch (SmtpException smtpEx)
            {
                throw new Exception($"Error de SMTP: {smtpEx.StatusCode} - {smtpEx.Message}", smtpEx);
            }
            catch (Exception ex)
            {
                throw new Exception($"Ocurrió un error al enviar el correo: {ex.Message}", ex);
            }
        }
    }
}