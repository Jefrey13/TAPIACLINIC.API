
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Logging;
    using System.Threading.Tasks;

    namespace Api.Middlewares
    {
        /// <summary>
        /// Middleware to log all incoming HTTP requests and outgoing responses.
        /// </summary>
        public class RequestLoggingMiddleware
        {
            private readonly RequestDelegate _next;
            private readonly ILogger<RequestLoggingMiddleware> _logger;

            /// <summary>
            /// Initializes the RequestLoggingMiddleware with the next delegate and logger.
            /// </summary>
            /// <param name="next">The next middleware in the pipeline.</param>
            /// <param name="logger">The logger instance.</param>
            public RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> logger)
            {
                _next = next;
                _logger = logger;
            }

            /// <summary>
            /// Invokes the middleware to log requests and responses.
            /// </summary>
            /// <param name="context">The HTTP context of the current request.</param>
            public async Task InvokeAsync(HttpContext context)
            {
                _logger.LogInformation($"Incoming Request: {context.Request.Method} {context.Request.Path}");
                await _next(context);
                _logger.LogInformation($"Outgoing Response: {context.Response.StatusCode}");
            }
        }
    }