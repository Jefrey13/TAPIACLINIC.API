using Application.Exceptions;
using Application.Models.ReponseDtos;
using AutoMapper;
using Domain.Entities;
using Domain.Repositories;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Queries.Users
{
    public class GetUsersByStateQueryHandler : IRequestHandler<GetUsersByStateQuery, IEnumerable<UserResponseDto>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public GetUsersByStateQueryHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<UserResponseDto>> Handle(GetUsersByStateQuery request, CancellationToken cancellationToken)
        {
            var users = await _userRepository.GetByStateAsync(request.StateId);

            if (users == null)
            {
                throw new NotFoundException(nameof(User), request.StateId);
            }

            return _mapper.Map<IEnumerable<UserResponseDto>>(users);
        }
    }
}