using System.Net;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;
using API.Utils;

namespace API.Middlewares
{
    /// <summary>
    /// Middleware para manejar excepciones globalmente y devolver respuestas estructuradas.
    /// </summary>
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

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

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var errorResponse = new ApiResponse<string>(
                success: false,
                message: "An unexpected error occurred.",
                statusCode: context.Response.StatusCode,
                errors: new Dictionary<string, string> { { "Error", exception.Message } }
            );

            return context.Response.WriteAsync(JsonConvert.SerializeObject(errorResponse));
        }
    }
}