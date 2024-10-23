namespace Application.Services
{
    public interface IEmailSender
    {
        /// <summary>
        /// Sends an email asynchronously.
        /// </summary>
        /// <param name="to">Recipient email address.</param>
        /// <param name="subject">Email subject.</param>
        /// <param name="body">Email body content.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task SendEmailAsync(string to, string subject, string body);
    }
}