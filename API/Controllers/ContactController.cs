using API.Utils;
using Application.Models.RequestDtos;
using Application.Models.ResponseDtos;
using Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly IContactService _contactService;
        public ContactController( IContactService contactService)
        {
            _contactService= contactService;
        }

        /// <summary>
        /// Activates a user's account and sends an activation message.
        /// </summary>
        /// <param name="contactRequestDto">The request object containing contact details.</param>
        /// <returns>An API response with the operation result.</returns>
        [HttpPost("SendMessageContact")]
        [AllowAnonymous]
        public async Task<ActionResult<ApiResponse<ServiceResult>>> SendMessageContact([FromBody] ContactRequestDto contactRequestDto, [FromHeader(Name = "RecaptchaToken")] string recaptchaToken)
        {
            if (contactRequestDto == null)
            {
                return ResponseHelper.BadRequest<ServiceResult>("El objeto de solicitud no puede estar vacío.");
            }

            if (!ModelState.IsValid)
            {
                return ResponseHelper.BadRequest<ServiceResult>("Datos de entrada inválidos. Por favor, verifica la información proporcionada.");
            }

            try
            {
                var result = await _contactService.SendMessageAsync(contactRequestDto, recaptchaToken);

                // Si reCAPTCHA es inválido, retornamos un error
                if (!result.Success)
                {
                    return ResponseHelper.Error<ServiceResult>("reCAPTCHA Invalido. Su intento no fue válido, por favor intente de nuevo.");
                }
                if (result == null)
                {
                    return ResponseHelper.Error<ServiceResult>("Error al enviar el mensaje de consulta");
                }
                if (result == null || !result.Success)
                {
                    return ResponseHelper.NotFound<ServiceResult>("Usuario no encontrado");
                }

                return ResponseHelper.Success(result, "El mensaje se envió correctamente.");
            }
            catch (Exception ex)
            {
                // Log exception (assumes a logging mechanism exists)
                //_logger.LogError(ex, "An error occurred during account activation.");

                return ResponseHelper.Error<ServiceResult>($"Ocurrió un error inesperado: {ex.Message}");
            }
        }
    }
}
