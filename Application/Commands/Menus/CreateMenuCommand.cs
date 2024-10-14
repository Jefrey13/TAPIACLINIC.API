using Application.Models;
using MediatR;

namespace Application.Commands.Menus
{
    /// <summary>
    /// Command to create a new menu.
    /// </summary>
    public class CreateMenuCommand : IRequest<int>
    {
        public MenuDto MenuDto { get; set; }

        public CreateMenuCommand(MenuDto menuDto)
        {
            MenuDto = menuDto;
        }
    }
}