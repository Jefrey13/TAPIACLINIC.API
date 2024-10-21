using Application.Models.ReponseDtos;
using Application.Queries.Staffs;
using AutoMapper;
using Domain.Repositories;
using MediatR;
using System.Threading.Tasks;

namespace Application.Queries.Staffs
{
    /// <summary>
    /// Handler for retrieving a staff member by User ID.
    /// </summary>
    public class GetStaffByUserIdQueryHandler : IRequestHandler<GetStaffByUserIdQuery, StaffResponseDto>
    {
        private readonly IStaffRepository _staffRepository;
        private readonly IMapper _mapper;

        public GetStaffByUserIdQueryHandler(IStaffRepository staffRepository, IMapper mapper)
        {
            _staffRepository = staffRepository;
            _mapper = mapper;
        }

        public async Task<StaffResponseDto> Handle(GetStaffByUserIdQuery request, CancellationToken cancellationToken)
        {
            // Retrieve the staff member associated with the given UserId
            var staff = await _staffRepository.GetStaffByUserIdAsync(request.UserId);

            // Map the staff member to a response DTO and return it
            return _mapper.Map<StaffResponseDto>(staff);
        }
    }
}
