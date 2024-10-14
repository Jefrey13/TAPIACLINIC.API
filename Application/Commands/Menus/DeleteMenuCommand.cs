using MediatR;

namespace Application.Commands.Menus
{
    /// <summary>
    /// Command to delete a menu.
    /// </summary>
    public class DeleteMenuCommand : IRequest<Unit>
    {
        public int Id { get; set; }

        public DeleteMenuCommand(int id)
        {
            Id = id;
        }
    }
}