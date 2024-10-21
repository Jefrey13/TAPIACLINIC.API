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
    /// Handler for retrieving all staff members, including user and specialty information.
    /// </summary>
    public class GetAllStaffsQueryHandler : IRequestHandler<GetAllStaffsQuery, IEnumerable<StaffResponseDto>>
    {
        private readonly IStaffRepository _staffRepository;
        private readonly IMapper _mapper;

        public GetAllStaffsQueryHandler(IStaffRepository staffRepository, IMapper mapper)
        {
            _staffRepository = staffRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<StaffResponseDto>> Handle(GetAllStaffsQuery request, CancellationToken cancellationToken)
        {
            // Retrieve all staff members with their related user and specialty information
            var staffs = await _staffRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<StaffResponseDto>>(staffs); // Map the result to DTOs
        }
    }
}