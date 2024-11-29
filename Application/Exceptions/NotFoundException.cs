namespace Application.Exceptions
{
    /// <summary>
    /// Represents an exception that occurs when a requested entity is not found in the system.
    /// </summary>
    public class NotFoundException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NotFoundException"/> class with a specified entity name and key.
        /// </summary>
        /// <param name="name">The name of the entity that was not found.</param>
        /// <param name="key">The key or identifier of the missing entity.</param>
        public NotFoundException(string name, object key)
            : base($"Entity \"{name}\" ({key}) was not found.")
        {
        }
    }
}