using Application.Commands.Menus;
using Application.Exceptions;
using AutoMapper;
using Domain.Entities;
using Domain.Repositories;
using MediatR;

namespace Application.Handlers.Menus
{
    /// <summary>
    /// Handler for updating an existing menu.
    /// </summary>
    public class UpdateMenuCommandHandler : IRequestHandler<UpdateMenuCommand, Unit>
    {
        private readonly IMenuRepository _menuRepository;
        private readonly IMapper _mapper;

        public UpdateMenuCommandHandler(IMenuRepository menuRepository, IMapper mapper)
        {
            _menuRepository = menuRepository;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateMenuCommand request, CancellationToken cancellationToken)
        {
            var menu = await _menuRepository.GetByIdAsync(request.Id);
            if (menu == null)
            {
                throw new NotFoundException(nameof(Menu), request.Id);
            }

            _mapper.Map(request.MenuDto, menu);
            menu.UpdatedAt = DateTime.Now;

            await _menuRepository.UpdateAsync(menu);
            return Unit.Value;
        }
    }
}