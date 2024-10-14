using MediatR;

namespace Application.Commands.Permissions
{
    /// <summary>
    /// Command to delete a permission by ID.
    /// </summary>
    public class DeletePermissionCommand : IRequest<Unit>
    {
        public int Id { get; set; }

        public DeletePermissionCommand(int id)
        {
            Id = id;
        }
    }
}