using Application.Models;
using Application.Models.ReponseDtos;
using AutoMapper;
using Domain.Repositories;
using MediatR;
using System.Collections.Generic;

namespace Application.Queries.Users
{
    /// <summary>
    /// Handler for retrieving all users.
    /// Maps the User entity to the UserResponseDto.
    /// </summary>
    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, IEnumerable<UserResponseDto>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public GetAllUsersQueryHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<UserResponseDto>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            var users = await _userRepository.GetAllAsync(request.RoleId, request.Id);
            return _mapper.Map<IEnumerable<UserResponseDto>>(users);
        }
    }
}