using Application.Exceptions;
using Application.Models;
using Application.Queries.Roles;
using AutoMapper;
using Domain.Entities;
using Domain.Repositories;
using MediatR;

namespace Application.Handlers.Roles
{
    /// <summary>
    /// Handler for retrieving a role by its ID.
    /// </summary>
    public class GetRoleByIdQueryHandler : IRequestHandler<GetRoleByIdQuery, RoleDto>
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IMapper _mapper;

        public GetRoleByIdQueryHandler(IRoleRepository roleRepository, IMapper mapper)
        {
            _roleRepository = roleRepository;
            _mapper = mapper;
        }

        public async Task<RoleDto> Handle(GetRoleByIdQuery request, CancellationToken cancellationToken)
        {
            var role = await _roleRepository.GetByIdAsync(request.Id);
            if (role == null)
            {
                throw new NotFoundException(nameof(Role), request.Id);
            }

            return _mapper.Map<RoleDto>(role);
        }
    }
}