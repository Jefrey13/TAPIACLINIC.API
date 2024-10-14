using MediatR;

namespace Application.Commands.Surgeries
{
    /// <summary>
    /// Command to delete a Surgery by ID.
    /// </summary>
    public class DeleteSurgeryCommand : IRequest<Unit>
    {
        public int Id { get; set; }

        public DeleteSurgeryCommand(int id)
        {
            Id = id;
        }
    }
}