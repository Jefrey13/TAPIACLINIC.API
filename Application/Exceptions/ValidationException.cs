using System;

namespace Application.Exceptions
{
    /// <summary>
    /// Represents an exception that occurs when data validation fails.
    /// Typically used for input validation errors or constraints violations in user input.
    /// </summary>
    public class ValidationException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationException"/> class with a specified validation error message.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the validation failure.</param>
        public ValidationException(string message) : base(message) { }
    }
}