using Application.Commands.Users;
using Application.Models;
using Application.Queries.Users;
using AutoMapper;
using MediatR;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Services.Impl
{
    /// <summary>
    /// Implements the operations related to managing users.
    /// This class uses MediatR for handling commands and queries, and AutoMapper for entity-to-DTO mapping.
    /// </summary>
    public class UserAppService : IUserAppService
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public UserAppService(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        public async Task<int> CreateUserAsync(CreateUserCommand command)
        {
            return await _mediator.Send(command);
        }

        public async Task UpdateUserAsync(UpdateUserCommand command)
        {
            await _mediator.Send(command);
        }

        public async Task DeleteUserAsync(DeleteUserCommand command)
        {
            await _mediator.Send(command);
        }

        public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
        {
            return await _mediator.Send(new GetAllUsersQuery());
        }

        public async Task<UserDto> GetUserByIdAsync(int id)
        {
            return await _mediator.Send(new GetUserByIdQuery(id));
        }
    }
}