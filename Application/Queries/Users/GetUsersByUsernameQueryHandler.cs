using Application.Exceptions;
using Application.Models.ReponseDtos;
using AutoMapper;
using Domain.Entities;
using Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.Users
{
    /// <summary>
    /// Handler for retrieving users based on their username.
    /// </summary>
    public class GetUsersByUsernameQueryHandler : IRequestHandler<GetUsersByUsernameQuery, UserResponseDto>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the handler with the user repository and mapper.
        /// </summary>
        public GetUsersByUsernameQueryHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Handles the query to retrieve user details by username.
        /// </summary>
        /// <param name="request">The request containing the username to search for.</param>
        /// <param name="cancellationToken">Token to monitor for cancellation requests.</param>
        /// <returns>A UserResponseDto containing the user details.</returns>
        /// <exception cref="NotFoundException">Thrown when the user is not found.</exception>
        public async Task<UserResponseDto> Handle(GetUsersByUsernameQuery request, CancellationToken cancellationToken)
        {
            try
            {
                // Retrieve the user from the repository based on the username
                var user = await _userRepository.GetUserByUserNameAsync(request.Username);

                // If the user is not found, throw a NotFoundException with a user-friendly message
                if (user == null)
                {
                    // Handle the case where the user is not found and show a friendly message in Spanish
                    throw new NotFoundException("Usuario no encontrado", $"El usuario con el nombre de usuario '{request.Username}' no se encuentra en nuestra base de datos. Por favor, verifique el nombre de usuario e intente nuevamente.");
                }

                // Map the retrieved user entity to a UserResponseDto
                return _mapper.Map<UserResponseDto>(user);
            }
            catch (NotFoundException ex)
            {
                // Handle the specific NotFoundException
                // Returning a friendly message for the user in Spanish
                throw new Exception($"Lo siento, no pudimos encontrar al usuario. {ex.Message}");
            }
            catch (Exception ex)
            {
                // General error handling
                // Display a generic error message in Spanish, so the user feels comfortable
                throw new Exception("Ocurrió un problema al intentar obtener los detalles del usuario. Por favor, inténtelo de nuevo más tarde.");
            }
        }
    }
}