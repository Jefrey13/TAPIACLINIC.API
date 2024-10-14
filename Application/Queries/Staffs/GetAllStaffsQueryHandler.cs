using Application.Models;
using Application.Queries.Staffs;
using AutoMapper;
using Domain.Repositories;
using MediatR;

namespace Application.Handlers.Staffs
{
    public class GetAllStaffsQueryHandler : IRequestHandler<GetAllStaffsQuery, IEnumerable<StaffDto>>
    {
        private readonly IStaffRepository _staffRepository;
        private readonly IMapper _mapper;

        public GetAllStaffsQueryHandler(IStaffRepository staffRepository, IMapper mapper)
        {
            _staffRepository = staffRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<StaffDto>> Handle(GetAllStaffsQuery request, CancellationToken cancellationToken)
        {
            var staffs = await _staffRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<StaffDto>>(staffs);
        }
    }
}