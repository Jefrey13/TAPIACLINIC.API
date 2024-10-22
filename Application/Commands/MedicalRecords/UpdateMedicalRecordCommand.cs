using Application.Models;
using Application.Models.RequestDtos;
using MediatR;

namespace Application.Commands.MedicalRecords
{
    /// <summary>
    /// Command to update an existing medical record.
    /// </summary>
    public class UpdateMedicalRecordCommand : IRequest<Unit>
    {
        public int Id { get; set; }
        public MedicalRecordRequestDto MedicalRecordDto { get; set; }

        /// <summary>
        /// Initializes a new instance of the UpdateMedicalRecordCommand class.
        /// </summary>
        /// <param name="id">The ID of the medical record to update.</param>
        /// <param name="dto">The DTO containing the updated details of the medical record.</param>
        public UpdateMedicalRecordCommand(int id, MedicalRecordRequestDto dto)
        {
            Id = id;
            MedicalRecordDto = dto;
        }
    }
}