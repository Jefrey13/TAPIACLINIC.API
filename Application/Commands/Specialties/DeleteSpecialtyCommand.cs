using MediatR;

namespace Application.Commands.Specialties
{
    /// <summary>
    /// Command to delete a Specialty by ID.
    /// </summary>
    public class DeleteSpecialtyCommand : IRequest<Unit>
    {
        public int Id { get; set; }

        public DeleteSpecialtyCommand(int id)
        {
            Id = id;
        }
    }
}