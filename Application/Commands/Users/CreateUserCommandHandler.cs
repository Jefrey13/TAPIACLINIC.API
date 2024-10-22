using Application.Commands.Users;
using AutoMapper;
using Domain.Entities;
using Domain.Repositories;
using MediatR;
using BCrypt.Net;

/// <summary>
/// Handler for creating a new user.
/// Maps the UserRequestDto to the User entity and saves it to the database.
/// </summary>
/// <summary>
/// Handler for creating a new user.
/// Maps the UserRequestDto to the User entity and saves it to the database.
/// </summary>
public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, int>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public CreateUserCommandHandler(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<int> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        // Map the UserRequestDto to the User entity
        var user = _mapper.Map<User>(request.UserDto);

        // Encrypt the password before saving it to the database
        user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);

        // Save the user to the repository
        await _userRepository.AddAsync(user);

        return user.Id;
    }
}