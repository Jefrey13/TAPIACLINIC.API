using Application.Exceptions;
using Application.Models;
using Application.Queries.Menus;
using AutoMapper;
using Domain.Repositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Handlers.Menus
{
    /// <summary>
    /// Handler for retrieving a menu by its ID.
    /// Maps from Menu entity to MenuDto.
    /// </summary>
    public class GetMenuByIdQueryHandler : IRequestHandler<GetMenuByIdQuery, MenuDto>
    {
        private readonly IMenuRepository _menuRepository;
        private readonly IMapper _mapper;

        public GetMenuByIdQueryHandler(IMenuRepository menuRepository, IMapper mapper)
        {
            _menuRepository = menuRepository;
            _mapper = mapper;
        }

        public async Task<MenuDto> Handle(GetMenuByIdQuery request, CancellationToken cancellationToken)
        {
            var menu = await _menuRepository.GetByIdAsync(request.Id);
            if (menu == null)
            {
                throw new NotFoundException(nameof(menu), request.Id);
            }

            return _mapper.Map<MenuDto>(menu);  // Map Menu entity to DTO
        }
    }
}