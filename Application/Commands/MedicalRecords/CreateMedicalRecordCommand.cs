using Application.Models;
using MediatR;

namespace Application.Commands.MedicalRecords
{
    /// <summary>
    /// Command to create a new medical record.
    /// </summary>
    public class CreateMedicalRecordCommand : IRequest<int>
    {
        public MedicalRecordDto MedicalRecordDto { get; set; }

        public CreateMedicalRecordCommand(MedicalRecordDto medicalRecordDto)
        {
            MedicalRecordDto = medicalRecordDto;
        }
    }
}