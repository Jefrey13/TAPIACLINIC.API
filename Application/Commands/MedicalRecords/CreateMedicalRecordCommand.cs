using Application.Models;
using Application.Models.RequestDtos;
using MediatR;

namespace Application.Commands.MedicalRecords
{
    /// <summary>
    /// Command to create a new medical record.
    /// </summary>
    public class CreateMedicalRecordCommand : IRequest<int>
    {
        public MedicalRecordRequestDto MedicalRecordDto { get; set; }

        /// <summary>
        /// Initializes a new instance of the CreateMedicalRecordCommand class.
        /// </summary>
        /// <param name="dto">The DTO containing the details of the medical record to create.</param>
        public CreateMedicalRecordCommand(MedicalRecordRequestDto medicalRecordDto)
        {
            MedicalRecordDto = medicalRecordDto;
        }
    }
}