﻿using Application.Commands.Users;
using Application.Models.ReponseDtos;
using Application.Models.RequestDtos;
using Application.Services;
using API.Utils;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Application.Models.RequestDtos.UpdateRequestDto;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserAppService _userAppService;
        public UsersController(IUserAppService userAppService)
        {
            _userAppService = userAppService;

        }

        /// <summary>
        /// Recupera todos los usuarios del sistema.
        /// </summary>
        /// <returns>Una lista de UserResponseDto representando a todos los usuarios.</returns>
        [HttpGet]
        public async Task<ActionResult<ApiResponse<IEnumerable<UserResponseDto>>>> GetAllUsers()
        {
            try
            {
                var jwtToken = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
                var users = await _userAppService.GetAllUsersAsync(jwtToken);

                if (users == null || !users.Any())
                {
                    return ResponseHelper.NotFound<IEnumerable<UserResponseDto>>("No se encontraron usuarios");
                }

                return ResponseHelper.Success(users, "Usuarios recuperados exitosamente");
            }
            catch (Exception ex)
            {
                return ResponseHelper.Error<IEnumerable<UserResponseDto>>($"Ocurrió un error: {ex.Message}");
            }
        }

        /// <summary>
        /// Recupera un usuario específico por su ID.
        /// </summary>
        /// <param name="id">El ID del usuario a recuperar.</param>
        /// <returns>Un UserResponseDto representando al usuario solicitado.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<UserResponseDto>>> GetUserById(int id)
        {
            if (id <= 0)
            {
                return ResponseHelper.BadRequest<UserResponseDto>("ID de usuario inválido");
            }

            try
            {
                var user = await _userAppService.GetUserByIdAsync(id);
                if (user == null)
                {
                    return ResponseHelper.NotFound<UserResponseDto>("Usuario no encontrado");
                }

                return ResponseHelper.Success(user, "Usuario recuperado exitosamente");
            }
            catch (Exception ex)
            {
                return ResponseHelper.Error<UserResponseDto>($"Ocurrió un error: {ex.Message}");
            }
        }

        /// <summary>
        /// Crea un nuevo usuario.
        /// </summary>
        /// <param name="userDto">El UserRequestDto que contiene los detalles del nuevo usuario.</param>
        /// <returns>Un ApiResponse que contiene el UserResponseDto del usuario creado.</returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<ApiResponse<UserResponseDto>>> CreateUser(
            [FromBody] UserRequestDto userDto,
            [FromHeader(Name = "RecaptchaToken")] string recaptchaToken)
        {
            // Validar el modelo recibido
            if (!ModelState.IsValid)
            {
                // Extraer errores de ModelState y retornarlos en la respuesta
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                return ResponseHelper.BadRequest<UserResponseDto>("Datos de usuario inválidos");
            }

            try
            {
                // Serializar e imprimir el objeto recibido para depuración
                var serializedUserDto = System.Text.Json.JsonSerializer.Serialize(userDto);
                Console.WriteLine($"Received user DTO: {serializedUserDto}");

                // Crear el usuario llamando al servicio de aplicación
                var userCreated = await _userAppService.CreateUserAsync(new CreateUserCommand(userDto), recaptchaToken);

                // Validar la respuesta del servicio
                if (userCreated == null)
                {
                    return ResponseHelper.Error<UserResponseDto>("Error al crear el usuario", 500);
                }

                if (!userCreated)
                {
                    return ResponseHelper.Error<UserResponseDto>("reCAPTCHA inválido. Por favor, intente nuevamente.", 400);
                }

                // Retornar éxito si todo es correcto
                return ResponseHelper.Success<UserResponseDto>(null, "Usuario creado exitosamente", 201);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}"); // Log de la excepción
                return ResponseHelper.Error<UserResponseDto>($"Error: {ex.Message}", 500);
            }
        }

        /// <summary>
        /// Crea un nuevo usuario como trabajador.
        /// </summary>
        /// <param name="userDto">El UserRequestDto que contiene los detalles del nuevo usuario.</param>
        /// <returns>Un ApiResponse que contiene el UserResponseDto del usuario creado.</returns>
        [HttpPost("CreateUserAsWorker")]
        public async Task<ActionResult<ApiResponse<UserResponseDto>>> CreateUserAsStaff([FromBody] UserRequestDto userDto)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                return ResponseHelper.BadRequest<UserResponseDto>("Datos de usuario inválidos");
            }

            try
            {
                // Lógica para crear usuario sin necesidad de reCAPTCHA
                var createdUser = await _userAppService.CreateUserReceptionistAsync(new CreateUserCommand(userDto));

                if (createdUser == null)
                {
                    return ResponseHelper.Error<UserResponseDto>("Error al crear el usuario", 500);
                }

                return ResponseHelper.Success<UserResponseDto>(null, "Usuario creado exitosamente", 201);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                return ResponseHelper.Error<UserResponseDto>($"Ocurrió un error inesperado: {ex.Message}", 500);
            }
        }


        /// <summary>
        /// Actualiza un usuario existente.
        /// </summary>
        /// <param name="id">El ID del usuario a actualizar.</param>
        /// <param name="userDto">El UserRequestDto que contiene los detalles actualizados del usuario.</param>
        /// <returns>Un ApiResponse que contiene el UserResponseDto del usuario actualizado.</returns>
        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResponse<UserResponseDto>>> UpdateUser(int id, [FromBody] UserUpdateRequestDto userDto)
        {
            if (id <= 0)
            {
                return ResponseHelper.BadRequest<UserResponseDto>("ID de usuario inválido");
            }

            try
            {
                var updatedUser = await _userAppService.UpdateUserAsync(new UpdateUserCommand(id, userDto));
                if (updatedUser == null)
                {
                    return ResponseHelper.NotFound<UserResponseDto>("Usuario no encontrado");
                }
                return ResponseHelper.Success(new UserResponseDto(), "Usuario actualizado exitosamente");
            }
            catch (Exception ex)
            {
                return ResponseHelper.Error<UserResponseDto>($"Ocurrió un error: {ex.Message}");
            }
        }

        /// <summary>
        /// Actualiza el estado de un usuario por su ID.
        /// </summary>
        /// <param name="id">El ID del usuario cuyo estado se desea actualizar.</param>
        /// <param name="newStateId">El nuevo estado del usuario.</param>
        /// <returns>Un ApiResponse que contiene el UserResponseDto del usuario con el estado actualizado.</returns>
        [HttpPatch("{id}/change-state")]
        public async Task<ActionResult<ApiResponse<UserResponseDto>>> UpdateUserState(int id)
        {
            if (id <= 0)
            {
                return ResponseHelper.BadRequest<UserResponseDto>("ID de usuario inválido");
            }

            try
            {
                var updatedUser = await _userAppService.DeleteUserAsync(new DeleteUserCommand(id));
                
                if (updatedUser == null)
                {
                    return ResponseHelper.NotFound<UserResponseDto>("Usuario no encontrado");
                }
                return ResponseHelper.Success(new UserResponseDto(), "Estado del usuario actualizado exitosamente");
            }
            catch (Exception ex)
            {
                return ResponseHelper.Error<UserResponseDto>($"Ocurrió un error: {ex.Message}");
            }
        }


        /// <summary>
        /// Recupera usuarios con el rol "Paciente" y el ID de estado especificado.
        /// </summary>
        /// <param name="stateId">El ID de estado de los usuarios a recuperar.</param>
        /// <returns>Una lista de UserResponseDto que representan a los usuarios con el rol "Paciente" en el estado especificado.</returns>
        [HttpGet("by-state/{stateId}")]
        public async Task<ActionResult<ApiResponse<IEnumerable<UserResponseDto>>>> GetUsersByState(int stateId)
        {
            if (stateId <= 0)
            {
                return ResponseHelper.BadRequest<IEnumerable<UserResponseDto>>("ID de estado inválido");
            }

            try
            {
                var users = await _userAppService.GetUsersByStateAsync(stateId);

                if (users == null || !users.Any())
                {
                    return ResponseHelper.NotFound<IEnumerable<UserResponseDto>>("No se encontraron usuarios para el estado especificado");
                }

                return ResponseHelper.Success(users, "Usuarios recuperados exitosamente");
            }
            catch (Exception ex)
            {
                return ResponseHelper.Error<IEnumerable<UserResponseDto>>($"Ocurrió un error: {ex.Message}");
            }
        }
    }
}