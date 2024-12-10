using Application.Commands.Users;
using Application.Models.ReponseDtos;
using Application.Queries.Users;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Services.Impl
{
    /// <summary>
    /// Service implementation for handling user operations.
    /// Utilizes MediatR to send commands and queries, and AutoMapper for entity-DTO conversions.
    /// </summary>
    public class UserAppService : IUserAppService
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        /// <summary>
        /// Constructor to initialize dependencies for MediatR and AutoMapper.
        /// </summary>
        /// <param name="mediator">MediatR instance to dispatch commands and queries.</param>
        /// <param name="mapper">AutoMapper instance to map between entities and DTOs.</param>
        public UserAppService(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        /// <summary>
        /// Sends a command to create a user.
        /// The command is handled asynchronously through MediatR.
        /// </summary>
        /// <param name="command">Command with user creation details.</param>
        /// <returns>Returns true if the user was created successfully.</returns>
        public async Task<bool> CreateUserAsync(CreateUserCommand command, string recaptchaToken)
        {
            // Verificar el token de reCAPTCHA antes de continuar con el registro
            var isRecaptchaValid = await _mediator.Send(new VerifyRecaptchaCommand(recaptchaToken));
            if (!isRecaptchaValid)
            {
                return false; // Si el reCAPTCHA no es válido, no registrar al usuario
            }
            // Si el reCAPTCHA es válido, continuar con la lógica de creación del usuario
            return await _mediator.Send(command); // Correcto, retorna un valor booleano indicando éxito o fallo
        }

        /// <summary>
        /// Sends a command to update a user.
        /// The command is dispatched asynchronously through MediatR.
        /// </summary>
        /// <param name="command">Command containing the updated user data.</param>
        /// <returns>Returns true if the user was updated successfully.</returns>
        public async Task<bool> UpdateUserAsync(UpdateUserCommand command)
        {
            return await _mediator.Send(command); // Ahora retorna bool para ser consistente con la interfaz
        }

        /// <summary>
        /// Sends a command to delete a user by their ID.
        /// The operation is asynchronous and is handled through MediatR.
        /// </summary>
        /// <param name="command">Command specifying the user ID to be deleted.</param>
        /// <returns>Returns true if the user was deleted successfully.</returns>
        public async Task<bool> DeleteUserAsync(DeleteUserCommand command)
        {
            return await _mediator.Send(command); // Ahora retorna bool para ser consistente con la interfaz
        }

        /// <summary>
        /// Sends a query to retrieve all users.
        /// The result is a collection of UserResponseDto containing user information.
        /// </summary>
        /// <returns>Returns a list of users with their details.</returns>
        public async Task<IEnumerable<UserResponseDto>> GetAllUsersAsync()
        {
            return await _mediator.Send(new GetAllUsersQuery());
        }

        /// <summary>
        /// Sends a query to retrieve a user by their ID.
        /// Returns detailed information about the user, or null if not found.
        /// </summary>
        /// <param name="id">The ID of the user to retrieve.</param>
        /// <returns>Returns a UserResponseDto with the user's details.</returns>
        public async Task<UserResponseDto> GetUserByIdAsync(int id)
        {
            return await _mediator.Send(new GetUserByIdQuery(id));
        }

        public async Task<IEnumerable<UserResponseDto>> GetUsersByStateAsync(int stateId)
        {
            return await _mediator.Send(new GetUsersByStateQuery(stateId));
        }

        public async Task<bool> UpdateUserIsAccountActivatedAsync(UpdateUserIsAccountActivatedCommand command)
        {
            return await _mediator.Send(command);
        }

        public async Task<bool> CreateUserReceptionistAsync(CreateUserCommand command)
        {
            return await _mediator.Send(command);
        }
    }
}