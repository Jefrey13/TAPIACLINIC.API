using Application.Models;
using MediatR;

namespace Application.Commands.MedicalRecords
{
    /// <summary>
    /// Command to update an existing medical record.
    /// </summary>
    public class UpdateMedicalRecordCommand : IRequest<Unit>
    {
        public int Id { get; set; }
        public MedicalRecordDto MedicalRecordDto { get; set; }

        public UpdateMedicalRecordCommand(int id, MedicalRecordDto medicalRecordDto)
        {
            Id = id;
            MedicalRecordDto = medicalRecordDto;
        }
    }
}