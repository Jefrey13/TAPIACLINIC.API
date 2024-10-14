using Application.Models;
using MediatR;

namespace Application.Queries.MedicalRecords
{
    /// <summary>
    /// Query to retrieve a medical record by its ID.
    /// </summary>
    public class GetMedicalRecordByIdQuery : IRequest<MedicalRecordDto>
    {
        public int Id { get; set; }

        public GetMedicalRecordByIdQuery(int id)
        {
            Id = id;
        }
    }
}