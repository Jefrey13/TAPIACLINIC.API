using Application.Exceptions;
using AutoMapper;
using Domain.Entities;
using Domain.Repositories;
using MediatR;

namespace Application.Commands.Users
{
    /// <summary>
    /// Handler for updating a user.
    /// Uses AutoMapper to map from UserDto to the User entity.
    /// </summary>
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, Unit>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UpdateUserCommandHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(request.Id);
            if (user == null)
            {
                throw new NotFoundException(nameof(User), request.Id);
            }

            // Map UserDto to existing User entity
            _mapper.Map(request.UserDto, user);

            // Update timestamp
            user.UpdatedAt = DateTime.Now;

            await _userRepository.UpdateAsync(user);
            return Unit.Value;
        }
    }
}