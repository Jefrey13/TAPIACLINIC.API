using Application.Models;
using Application.Models.ReponseDtos;
using MediatR;
using System.Collections.Generic;

namespace Application.Queries.MedicalRecords
{
    /// <summary>
    /// Query to retrieve all medical records.
    /// </summary>
    public class GetAllMedicalRecordsQuery : IRequest<IEnumerable<MedicalRecordResponseDto>> { }
}