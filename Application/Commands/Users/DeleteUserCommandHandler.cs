using Application.Commands.Users;
using Application.Exceptions;
using Domain.Entities;
using Domain.Repositories;
using MediatR;
using System;

namespace Application.Handlers.Users
{
    /// <summary>
    /// Handler for deleting a user by their ID.
    /// This class handles the `DeleteUserCommand`.
    /// </summary>
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, bool>
    {
        private readonly IUserRepository _userRepository;

        public DeleteUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<bool> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                // Validate the incoming ID
                if (request.Id <= 0)
                {
                    throw new ValidationException("Invalid user ID provided.");
                }

                // Retrieve the user by ID
                var user = await _userRepository.GetByIdAsync(request.Id);

                // Check if the user exists
                if (user == null)
                {
                    throw new NotFoundException(nameof(User), request.Id);
                }

                // Toggle the active state of the user
                await _userRepository.ToggleActiveStateAsync(user);

                // Validate if the state was successfully toggled
                var updatedUser = await _userRepository.GetByIdAsync(request.Id);

                if (updatedUser == null)
                {
                    throw new OperationFailedException($"Failed to toggle the active state for user with ID {request.Id}.");
                }

                return true;
            }
            catch (ValidationException ex)
            {
                // Handle validation-specific errors
                throw new ValidationException($"Validation error: {ex.Message}");
            }
            catch (NotFoundException ex)
            {
                // Handle not found-specific errors
                throw new NotFoundException($"User not found: {ex.Message}", request.Id);
            }
            catch (OperationFailedException ex)
            {
                // Handle failed operations
                throw new OperationFailedException($"Operation error: {ex.Message}");
            }
            catch (Exception ex)
            {
                // Log and rethrow unexpected exceptions
                throw new Exception($"An unexpected error occurred while deleting the user: {ex.Message}", ex);
            }
        }
    }
}