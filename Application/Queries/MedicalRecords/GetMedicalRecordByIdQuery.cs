using Application.Models;
using Application.Models.ReponseDtos;
using MediatR;

namespace Application.Queries.MedicalRecords
{
    /// <summary>
    /// Query to retrieve a medical record by its ID.
    /// </summary>
    public class GetMedicalRecordByIdQuery : IRequest<MedicalRecordResponseDto>
    {
        public int Id { get; set; }

        /// <summary>
        /// Initializes a new instance of the GetMedicalRecordByIdQuery class.
        /// </summary>
        /// <param name="id">The ID of the medical record to retrieve.</param>
        public GetMedicalRecordByIdQuery(int id)
        {
            Id = id;
        }
    }
}