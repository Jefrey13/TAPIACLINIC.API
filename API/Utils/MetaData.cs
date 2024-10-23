namespace API.Utils
{
    /// <summary>
    /// Class to hold additional metadata for the response, such as pagination information.
    /// </summary>
    public class MetaData
    {
        /// <summary>
        /// The current page number in paginated data.
        /// </summary>
        public int Page { get; set; }

        /// <summary>
        /// The number of items per page in paginated data.
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// The total number of pages in paginated data.
        /// </summary>
        public int TotalPages { get; set; }

        /// <summary>
        /// The total number of items in the result set.
        /// </summary>
        public int TotalCount { get; set; }

        /// <summary>
        /// A unique identifier for tracking the request, useful for logging or debugging.
        /// </summary>
        public string RequestId { get; set; }
    }
}