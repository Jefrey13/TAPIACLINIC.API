using Application.Models;
using AutoMapper;
using Domain.Repositories;
using MediatR;
using System.Collections.Generic;

namespace Application.Queries.Users
{
    /// <summary>
    /// Handler for retrieving all users.
    /// Maps from User entity to UserDto.
    /// </summary>
    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, IEnumerable<UserDto>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public GetAllUsersQueryHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<UserDto>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            var users = await _userRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<UserDto>>(users);  // Map User entities to DTOs
        }
    }
}