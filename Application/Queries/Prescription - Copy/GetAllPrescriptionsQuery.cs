using Application.Models.ResponseDtos;
using MediatR;
using System.Collections.Generic;

namespace Application.Queries.Prescriptions
{
    /// <summary>
    /// Query to retrieve all prescriptions.
    /// </summary>
    public class GetAllPrescriptionsQuery : IRequest<IEnumerable<PrescriptionResponseDto>>
    {
    }
}