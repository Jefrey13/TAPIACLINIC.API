using System;

namespace Application.Exceptions
{
    /// <summary>
    /// Represents an exception that occurs when a conflict is detected during an operation.
    /// Typically used for scenarios like duplicate entries or constraint violations.
    /// </summary>
    public class ConflictException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ConflictException"/> class with a specified error message.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the conflict.</param>
        public ConflictException(string message) : base(message) { }
    }
}