using MediatR;

namespace Application.Commands.MedicalRecords
{
    /// <summary>
    /// Command to delete (or deactivate) a medical record by ID.
    /// </summary>
    public class DeleteMedicalRecordCommand : IRequest<Unit>
    {
        public int Id { get; set; }

        public DeleteMedicalRecordCommand(int id)
        {
            Id = id;
        }
    }
}