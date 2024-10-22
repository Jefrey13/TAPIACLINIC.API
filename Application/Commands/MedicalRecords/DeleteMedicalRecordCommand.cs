using MediatR;

namespace Application.Commands.MedicalRecords
{
    /// <summary>
    /// Command to delete a medical record.
    /// </summary>
    public class DeleteMedicalRecordCommand : IRequest<Unit>
    {
        public int Id { get; set; }

        /// <summary>
        /// Initializes a new instance of the DeleteMedicalRecordCommand class.
        /// </summary>
        /// <param name="id">The ID of the medical record to delete.</param>
        public DeleteMedicalRecordCommand(int id)
        {
            Id = id;
        }
    }
}