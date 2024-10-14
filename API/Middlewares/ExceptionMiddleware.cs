using Newtonsoft.Json;
using System.Net;

namespace API.Middlewares
{
    /// <summary>
    /// Middleware to handle unhandled exceptions and return a structured error response.
    /// </summary>
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        /// <summary>
        /// Initializes the ExceptionMiddleware with the next delegate in the pipeline.
        /// </summary>
        /// <param name="next">The next middleware in the request pipeline.</param>
        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        /// <summary>
        /// Invokes the middleware, catching any unhandled exceptions and returning a proper response.
        /// </summary>
        /// <param name="context">The HTTP context of the current request.</param>
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        /// <summary>
        /// Handles the exception and writes a JSON response with status code and error details.
        /// </summary>
        /// <param name="context">The HTTP context where the exception occurred.</param>
        /// <param name="exception">The exception that was thrown.</returns>
        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var response = new
            {
                StatusCode = context.Response.StatusCode,
                Message = "Internal Server Error",
                Details = exception.Message
            };

            return context.Response.WriteAsync(JsonConvert.SerializeObject(response));
        }
    }
}
