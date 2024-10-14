using Application.Models;
using MediatR;
using System.Collections.Generic;

namespace Application.Queries.MedicalRecords
{
    /// <summary>
    /// Query to retrieve all medical records.
    /// </summary>
    public class GetAllMedicalRecordsQuery : IRequest<IEnumerable<MedicalRecordDto>>
    {
    }
}