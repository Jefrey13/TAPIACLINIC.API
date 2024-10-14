using MediatR;

namespace Application.Commands.Exams
{
    /// <summary>
    /// Command to delete an exam by ID.
    /// </summary>
    public class DeleteExamCommand : IRequest<Unit>
    {
        public int Id { get; set; }

        public DeleteExamCommand(int id)
        {
            Id = id;
        }
    }
}