using Application.Commands.Users;
using Application.Exceptions;
using AutoMapper;
using Domain.Entities;
using Domain.Repositories;
using MediatR;
using BCrypt.Net;

namespace Application.Handlers.Users
{
    /// <summary>
    /// Handler for creating a new user.
    /// Maps the UserRequestDto to the User entity and saves it to the database.
    /// </summary>
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, bool>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public CreateUserCommandHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<bool> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                // Validate incoming data
                if (string.IsNullOrWhiteSpace(request.UserDto.FirstName))
                {
                    throw new ValidationException("First name is required.");
                }

                if (string.IsNullOrWhiteSpace(request.UserDto.Email))
                {
                    throw new ValidationException("Email is required.");
                }

                // Check if the email or username already exists
                var existingUserByEmail = await _userRepository.GetUserByEmailAsync(request.UserDto.Email);
                if (existingUserByEmail != null)
                {
                    throw new ConflictException("A user with the same email already exists.");
                }

                var existingUserByUserName = await _userRepository.GetUserByUserNameAsync(request.UserDto.UserName);
                if (existingUserByUserName != null)
                {
                    throw new ConflictException("A user with the same username already exists.");
                }

                // Map the UserRequestDto to the User entity
                var user = _mapper.Map<User>(request.UserDto);

                // Encrypt the password before saving it to the database
                user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);

                // Set creation and update timestamps
                user.CreatedAt = DateTime.Now;
                user.UpdatedAt = DateTime.Now;

                // Save the user to the repository
                await _userRepository.AddAsync(user);

                // Check if the user was successfully added
                if (user.Id <= 0)
                {
                    throw new Exception("Failed to create the user.");
                }

                return true;
            }
            catch (ValidationException ex)
            {
                // Log validation errors and rethrow
                throw new ValidationException($"Validation error: {ex.Message}");
            }
            catch (ConflictException ex)
            {
                // Log conflict errors (e.g., duplicate email or username) and rethrow
                throw new ConflictException($"Conflict error: {ex.Message}");
            }
            catch (Exception ex)
            {
                // Log generic errors and rethrow as a custom exception
                throw new Exception($"An unexpected error occurred while creating the user: {ex.Message}", ex);
            }
        }
    }
}