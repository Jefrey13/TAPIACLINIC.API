namespace API.Utils
{
    /// <summary>
    /// A generic class to structure API responses in a consistent format.
    /// </summary>
    /// <typeparam name="T">The type of data being returned in the response.</typeparam>
    public class ApiResponse<T>
    {
        /// <summary>
        /// Indicates if the request was successful or not.
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// A message with more details about the response (can be a success message or an error message).
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// The actual data returned in the response (if any).
        /// </summary>
        public T Data { get; set; }

        /// <summary>
        /// HTTP status code of the response (e.g., 200 for OK, 404 for Not Found, 500 for server error).
        /// </summary>
        public int StatusCode { get; set; }

        /// <summary>
        /// A collection of error details in case of an error response.
        /// </summary>
        public Dictionary<string, string> Errors { get; set; }

        /// <summary>
        /// Additional metadata such as pagination info or request details.
        /// </summary>
        public MetaData Meta { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public ApiResponse() { }

        /// <summary>
        /// Constructor for a successful response without metadata or errors.
        /// </summary>
        /// <param name="success">Indicates if the request was successful.</param>
        /// <param name="message">The message describing the result of the request.</param>
        /// <param name="data">The data being returned.</param>
        /// <param name="statusCode">The HTTP status code.</param>
        public ApiResponse(bool success, string message, T data, int statusCode)
        {
            Success = success;
            Message = message;
            Data = data;
            StatusCode = statusCode;
            Errors = null;
            Meta = null;
        }

        /// <summary>
        /// Constructor for a failed response with error details.
        /// </summary>
        /// <param name="success">Indicates if the request was successful.</param>
        /// <param name="message">The error message describing the failure.</param>
        /// <param name="statusCode">The HTTP status code for the error.</param>
        /// <param name="errors">The dictionary of error details.</param>
        public ApiResponse(bool success, string message, int statusCode, Dictionary<string, string> errors)
        {
            Success = success;
            Message = message;
            Data = default;
            StatusCode = statusCode;
            Errors = errors;
            Meta = null;
        }

        /// <summary>
        /// Constructor for a response with additional metadata.
        /// </summary>
        /// <param name="success">Indicates if the request was successful.</param>
        /// <param name="message">The message describing the result of the request.</param>
        /// <param name="data">The data being returned.</param>
        /// <param name="statusCode">The HTTP status code.</param>
        /// <param name="meta">Additional metadata like pagination information.</param>
        public ApiResponse(bool success, string message, T data, int statusCode, MetaData meta)
        {
            Success = success;
            Message = message;
            Data = data;
            StatusCode = statusCode;
            Errors = null;
            Meta = meta;
        }

        public ApiResponse(bool success, string message, T data, int statusCode, Dictionary<string, string> errors)
        {
            Success = success;
            Message = message;
            Data = data;
            StatusCode = statusCode;
            Errors = errors;
            Meta = null;
        }

    }
}
