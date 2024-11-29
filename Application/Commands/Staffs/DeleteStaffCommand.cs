using MediatR;

namespace Application.Commands.Staffs
{
    /// <summary>
    /// Command to delete a staff member by ID.
    /// </summary>
    public class DeleteStaffCommand : IRequest<bool>
    {
        public int Id { get; set; }

        public DeleteStaffCommand(int id)
        {
            Id = id;
        }
    }
}