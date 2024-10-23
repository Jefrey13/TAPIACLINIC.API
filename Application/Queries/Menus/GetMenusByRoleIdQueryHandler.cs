using Application.Models;
using Application.Models.ReponseDtos;
using AutoMapper;
using Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.Menus
{
    public class GetMenusByRoleIdQueryHandler : IRequestHandler<GetMenusByRoleIdQuery, IEnumerable<MenuResponseDto>>
    {
        private readonly IMenuRepository _menuRepository;
        private readonly IMapper _mapper;

        public GetMenusByRoleIdQueryHandler(IMenuRepository menuRepository, IMapper mapper)
        {
            _menuRepository = menuRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<MenuResponseDto>> Handle(GetMenusByRoleIdQuery request, CancellationToken cancellationToken)
        {
            // Obtenemos los menús basados en el RoleId proporcionado
            var menus = await _menuRepository.GetMenusByRoleIdAsync(request.RoleId);

            // Mappeamos los menús a DTOs
            return _mapper.Map<IEnumerable<MenuResponseDto>>(menus);
        }
    }
}
