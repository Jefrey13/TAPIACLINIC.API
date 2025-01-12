using Application.Models.ReponseDtos;
using MediatR;
using System.Collections.Generic;

namespace Application.Queries.MedicalRecords
{
    /// <summary>
    /// Query to retrieve medical records based on a patient's ID.
    /// </summary>
    public class GetMedicalRecordsByPatientIdQuery : IRequest<List<MedicalRecordResponseDto>>
    {
        public int PatientId { get; set; }

        /// <summary>
        /// Initializes a new instance of the GetMedicalRecordsByPatientIdQuery class.
        /// </summary>
        /// <param name="patientId">The ID of the patient whose medical records are being retrieved.</param>
        public GetMedicalRecordsByPatientIdQuery(int patientId)
        {
            PatientId = patientId;
        }
    }
}