using Application.Models;
using MediatR;

namespace Application.Commands.Menus
{
    /// <summary>
    /// Command to update an existing menu.
    /// </summary>
    public class UpdateMenuCommand : IRequest<Unit>
    {
        public int Id { get; set; }
        public MenuDto MenuDto { get; set; }

        public UpdateMenuCommand(int id, MenuDto menuDto)
        {
            Id = id;
            MenuDto = menuDto;
        }
    }
}