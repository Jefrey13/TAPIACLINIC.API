using Microsoft.AspNetCore.Mvc;

namespace API.Utils
{
    public static class ResponseHelper
    {
        public static ActionResult<ApiResponse<T>> Success<T>(T data, string message = "Operación exitosa", int statusCode = 200, MetaData meta = null)
        {
            var response = new ApiResponse<T>(true, message, data, statusCode, meta);
            return new ObjectResult(response) { StatusCode = statusCode };
        }

        public static ActionResult<ApiResponse<T>> Error<T>(string message, int statusCode = 500, Dictionary<string, string> errors = null)
        {
            var response = new ApiResponse<T>(false, message, default, statusCode, errors);
            return new ObjectResult(response) { StatusCode = statusCode };
        }

        public static ActionResult<ApiResponse<T>> NotFound<T>(string message = "Recurso no encontrado")
        {
            return Error<T>(message, 404);
        }

        public static ActionResult<ApiResponse<T>> BadRequest<T>(string message = "Solicitud inválida", Dictionary<string, string> errors = null)
        {
            return Error<T>(message, 400, errors);
        }
    }
}
