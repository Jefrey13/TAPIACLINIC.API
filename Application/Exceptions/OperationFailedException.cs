using System;

namespace Application.Exceptions
{
    /// <summary>
    /// Represents an exception that occurs when an operation fails to complete successfully.
    /// Typically used for unexpected errors during data manipulation or business logic execution.
    /// </summary>
    public class OperationFailedException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OperationFailedException"/> class with a specified error message.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the failure.</param>
        public OperationFailedException(string message) : base(message) { }
    }
}