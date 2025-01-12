using Application.Models.ResponseDtos;
using MediatR;

namespace Application.Queries.Prescriptions
{
    /// <summary>
    /// Query to retrieve a prescription by its ID.
    /// </summary>
    public class GetPrescriptionByIdQuery : IRequest<PrescriptionResponseDto>
    {
        public int Id { get; set; }

        /// <summary>
        /// Initializes a new instance of the GetPrescriptionByIdQuery class.
        /// </summary>
        /// <param name="id">The ID of the prescription to retrieve.</param>
        public GetPrescriptionByIdQuery(int id)
        {
            Id = id;
        }
    }
}