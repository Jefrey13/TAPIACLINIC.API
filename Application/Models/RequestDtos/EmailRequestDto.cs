namespace Application.Models.RequestDtos
{
    /// <summary>
    /// DTO representing the email request information.
    /// </summary>
    public class EmailRequestDto
    {
        /// <summary>
        /// Gets or sets the email address to send the email to.
        /// </summary>
        public string To { get; set; }

        /// <summary>
        /// Gets or sets the subject of the email.
        /// </summary>
        public string Subject { get; set; }

        /// <summary>
        /// Gets or sets the body content of the email.
        /// </summary>
        public string Body { get; set; }
    }
}