using Application.Commands.Users;
using AutoMapper;
using Domain.Entities;
using Domain.Repositories;

using MediatR;

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
        var user = _mapper.Map<User>(request.UserDto);
        await _userRepository.AddAsync(user);
        return user.Id;
    }
}