using Application.Models.ReponseDtos;
using Application.Queries.Staffs;
using AutoMapper;
using Domain.Repositories;
using MediatR;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Queries.Staffs
{
    /// <summary>
    /// Handler for retrieving staff members by Specialty ID.
    /// </summary>
    public class GetStaffBySpecialtyIdQueryHandler : IRequestHandler<GetStaffBySpecialtyIdQuery, IEnumerable<StaffResponseDto>>
    {
        private readonly IStaffRepository _staffRepository;
        private readonly IMapper _mapper;

        public GetStaffBySpecialtyIdQueryHandler(IStaffRepository staffRepository, IMapper mapper)
        {
            _staffRepository = staffRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<StaffResponseDto>> Handle(GetStaffBySpecialtyIdQuery request, CancellationToken cancellationToken)
        {
            // Retrieve staff members associated with the given SpecialtyId
            var staffs = await _staffRepository.GetStaffBySpecialtyIdAsync(request.SpecialtyId);

            // Map the staff members to response DTOs and return them
            return _mapper.Map<IEnumerable<StaffResponseDto>>(staffs);
        }
    }
}