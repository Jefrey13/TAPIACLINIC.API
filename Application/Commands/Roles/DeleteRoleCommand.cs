using MediatR;

namespace Application.Commands.Roles
{
    /// <summary>
    /// Command to delete a role by ID.
    /// </summary>
    public class DeleteRoleCommand : IRequest<Unit>
    {
        public int Id { get; set; }

        public DeleteRoleCommand(int id)
        {
            Id = id;
        }
    }
}