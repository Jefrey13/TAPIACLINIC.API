using Application.Models.ReponseDtos;
using MediatR;
using System.Collections.Generic;

namespace Application.Queries.Staffs
{
    /// <summary>
    /// Query to get staff members by Specialty ID.
    /// </summary>
    public class GetStaffBySpecialtyIdQuery : IRequest<IEnumerable<StaffResponseDto>>
    {
        public int SpecialtyId { get; set; }

        public GetStaffBySpecialtyIdQuery(int specialtyId)
        {
            SpecialtyId = specialtyId;
        }
    }
}