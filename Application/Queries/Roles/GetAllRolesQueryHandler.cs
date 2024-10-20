    using Application.Models.ReponseDtos;
    using Application.Queries.Roles;
    using AutoMapper;
    using Domain.Repositories;
    using MediatR;

    namespace Application.Handlers.Roles
    {
        /// <summary>
        /// Handler for retrieving all roles.
        /// Maps from Role entity to RoleResponseDto.
        /// </summary>
        public class GetAllRolesQueryHandler : IRequestHandler<GetAllRolesQuery, IEnumerable<RoleResponseDto>>
        {
            private readonly IRoleRepository _roleRepository;
            private readonly IMapper _mapper;

            public GetAllRolesQueryHandler(IRoleRepository roleRepository, IMapper mapper)
            {
                _roleRepository = roleRepository;
                _mapper = mapper;
            }

            public async Task<IEnumerable<RoleResponseDto>> Handle(GetAllRolesQuery request, CancellationToken cancellationToken)
            {
                var roles = await _roleRepository.GetAllAsync();
                return _mapper.Map<IEnumerable<RoleResponseDto>>(roles);
            }
        }
    }