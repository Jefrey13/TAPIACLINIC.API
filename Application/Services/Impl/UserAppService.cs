using Application.Commands.Users;
using Application.Models.ReponseDtos;
using Application.Queries.Users;
using AutoMapper;
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
        /// <returns>Returns the ID of the created user.</returns>
        public async Task<int> CreateUserAsync(CreateUserCommand command)
        {
            return await _mediator.Send(command);
        }

        /// <summary>
        /// Sends a command to update a user.
        /// The command is dispatched asynchronously through MediatR and does not return any data.
        /// </summary>
        /// <param name="command">Command containing the updated user data.</param>
        public async Task UpdateUserAsync(UpdateUserCommand command)
        {
            await _mediator.Send(command);
        }

        /// <summary>
        /// Sends a command to delete a user by their ID.
        /// The operation is asynchronous and is handled through MediatR.
        /// </summary>
        /// <param name="command">Command specifying the user ID to be deleted.</param>
        public async Task DeleteUserAsync(DeleteUserCommand command)
        {
            await _mediator.Send(command);
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
    }
}