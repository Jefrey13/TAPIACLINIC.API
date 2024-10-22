using Application.Models.ReponseDtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.MedicalRecords
{
    /// <summary>
    /// Query to retrieve a medical record based on a patient's ID.
    /// </summary>
    public class GetMedicalRecordByPatientIdQuery : IRequest<MedicalRecordResponseDto>
    {
        public int PatientId { get; set; }

        /// <summary>
        /// Initializes a new instance of the GetMedicalRecordByPatientIdQuery class.
        /// </summary>
        /// <param name="patientId">The ID of the patient whose medical record is being retrieved.</param>
        public GetMedicalRecordByPatientIdQuery(int patientId)
        {
            PatientId = patientId;
        }
    }
}
