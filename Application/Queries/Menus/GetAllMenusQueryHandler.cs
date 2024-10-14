using Application.Models;
using Application.Queries.Menus;
using AutoMapper;
using Domain.Repositories;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Handlers.Menus
{
    /// <summary>
    /// Handler for retrieving all menus.
    /// Maps from Menu entity to MenuDto.
    /// </summary>
    public class GetAllMenusQueryHandler : IRequestHandler<GetAllMenusQuery, IEnumerable<MenuDto>>
    {
        private readonly IMenuRepository _menuRepository;
        private readonly IMapper _mapper;

        public GetAllMenusQueryHandler(IMenuRepository menuRepository, IMapper mapper)
        {
            _menuRepository = menuRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<MenuDto>> Handle(GetAllMenusQuery request, CancellationToken cancellationToken)
        {
            var menus = await _menuRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<MenuDto>>(menus);  // Map Menu entities to DTOs
        }
    }
}