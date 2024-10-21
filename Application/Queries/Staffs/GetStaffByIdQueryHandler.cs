using Application.Exceptions;
using Application.Queries.Staffs;
using AutoMapper;
using Domain.Repositories;
using MediatR;
using System.Threading.Tasks;
using Application.Models.ReponseDtos;
using Domain.Entities;

namespace Application.Queries.Staffs
{
    /// <summary>
    /// Handler for retrieving a staff member by ID.
    /// </summary>
    public class GetStaffByIdQueryHandler : IRequestHandler<GetStaffByIdQuery, StaffResponseDto>
    {
        private readonly IStaffRepository _staffRepository;
        private readonly IMapper _mapper;

        public GetStaffByIdQueryHandler(IStaffRepository staffRepository, IMapper mapper)
        {
            _staffRepository = staffRepository;
            _mapper = mapper;
        }

        public async Task<StaffResponseDto> Handle(GetStaffByIdQuery request, CancellationToken cancellationToken)
        {
            // Check if the staff member exists
            var staff = await _staffRepository.GetByIdAsync(request.Id);

            if (staff == null)
            {
                throw new NotFoundException(nameof(Staff), request.Id);
            }

            // Map the staff member to a response DTO and return it
            return _mapper.Map<StaffResponseDto>(staff);
        }
    }
}