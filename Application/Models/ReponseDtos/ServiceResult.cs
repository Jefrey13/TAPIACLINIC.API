namespace Application.Models.ResponseDtos
{
    /// <summary>
    /// Represents the result of a service operation.
    /// </summary>
    public class ServiceResult
    {
        /// <summary>
        /// Indicates whether the operation was successful.
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// Provides additional information about the operation result.
        /// </summary>
        public string Message { get; set; }
    }
}