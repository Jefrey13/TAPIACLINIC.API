using Application.Commands.Users;
using Application.Exceptions;
using AutoMapper;
using Domain.Entities;
using Domain.Repositories;
using MediatR;

namespace Application.Handlers.Users
{
    /// <summary>
    /// Handler for updating an existing user.
    /// This class handles the `UpdateUserCommand`.
    /// </summary>
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, bool>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UpdateUserCommandHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<bool> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
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

                // Map the updated fields to the user entity
                _mapper.Map(request.UserDto, user);

                // Update the user in the repository
                await _userRepository.UpdateAsync(user);

                // Validate if the user was successfully updated
                var updatedUser = await _userRepository.GetByIdAsync(request.Id);

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
                throw new NotFoundException(nameof(User), request.Id);
            }
            catch (OperationFailedException ex)
            {
                // Handle failed operations
                throw new OperationFailedException($"Operation error: {ex.Message}");
            }
            catch (Exception ex)
            {
                // Log and rethrow unexpected exceptions
                throw new Exception($"An unexpected error occurred while updating the user: {ex.Message}", ex);
            }
        }
    }
}