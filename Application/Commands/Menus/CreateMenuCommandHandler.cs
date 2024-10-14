using Application.Commands.Menus;
using AutoMapper;
using Domain.Entities;
using Domain.Repositories;
using MediatR;

namespace Application.Handlers.Menus
{
    /// <summary>
    /// Handler for creating a new menu.
    /// </summary>
    public class CreateMenuCommandHandler : IRequestHandler<CreateMenuCommand, int>
    {
        private readonly IMenuRepository _menuRepository;
        private readonly IMapper _mapper;

        public CreateMenuCommandHandler(IMenuRepository menuRepository, IMapper mapper)
        {
            _menuRepository = menuRepository;
            _mapper = mapper;
        }

        public async Task<int> Handle(CreateMenuCommand request, CancellationToken cancellationToken)
        {
            var menu = _mapper.Map<Menu>(request.MenuDto);
            menu.CreatedAt = DateTime.Now;
            menu.UpdatedAt = DateTime.Now;

            await _menuRepository.AddAsync(menu);
            return menu.Id;
        }
    }
}