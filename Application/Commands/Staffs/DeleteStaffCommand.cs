using MediatR;

namespace Application.Commands.Staffs
{
    /// <summary>
    /// Command to delete an existing staff.
    /// </summary>
    public class DeleteStaffCommand : IRequest<Unit>
    {
        public int Id { get; set; }

        public DeleteStaffCommand(int id)
        {
            Id = id;
        }
    }
}