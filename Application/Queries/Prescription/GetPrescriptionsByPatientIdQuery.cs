using Application.Models.ResponseDtos;
using MediatR;
using System.Collections.Generic;

namespace Application.Queries.Prescriptions
{
    /// <summary>
    /// Query to retrieve prescriptions by patient ID.
    /// </summary>
    public class GetPrescriptionsByPatientIdQuery : IRequest<IEnumerable<PrescriptionResponseDto>>
    {
        public int PatientId { get; set; }

        /// <summary>
        /// Initializes a new instance of the GetPrescriptionsByPatientIdQuery class.
        /// </summary>
        /// <param name="patientId">The ID of the patient whose prescriptions are to be retrieved.</param>
        public GetPrescriptionsByPatientIdQuery(int patientId)
        {
            PatientId = patientId;
        }
    }
}
