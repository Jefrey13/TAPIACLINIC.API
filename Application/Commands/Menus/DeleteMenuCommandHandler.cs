using Application.Commands.Menus;
using Application.Exceptions;
using Domain.Entities;
using Domain.Repositories;
using MediatR;

namespace Application.Handlers.Menus
{
    /// <summary>
    /// Handler for deleting a menu.
    /// </summary>
    public class DeleteMenuCommandHandler : IRequestHandler<DeleteMenuCommand, Unit>
    {
        private readonly IMenuRepository _menuRepository;

        public DeleteMenuCommandHandler(IMenuRepository menuRepository)
        {
            _menuRepository = menuRepository;
        }

        public async Task<Unit> Handle(DeleteMenuCommand request, CancellationToken cancellationToken)
        {
            var menu = await _menuRepository.GetByIdAsync(request.Id);
            if (menu == null)
            {
                throw new NotFoundException(nameof(Menu), request.Id);
            }

            await _menuRepository.DeleteAsync(menu);
            return Unit.Value;
        }
    }
}