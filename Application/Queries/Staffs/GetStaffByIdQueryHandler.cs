using Application.Models;
using Application.Exceptions;
using Application.Queries.Staffs;
using AutoMapper;
using Domain.Repositories;
using MediatR;
using Domain.Entities;

namespace Application.Handlers.Staffs
{
    public class GetStaffByIdQueryHandler : IRequestHandler<GetStaffByIdQuery, StaffDto>
    {
        private readonly IStaffRepository _staffRepository;
        private readonly IMapper _mapper;

        public GetStaffByIdQueryHandler(IStaffRepository staffRepository, IMapper mapper)
        {
            _staffRepository = staffRepository;
            _mapper = mapper;
        }

        public async Task<StaffDto> Handle(GetStaffByIdQuery request, CancellationToken cancellationToken)
        {
            var staff = await _staffRepository.GetByIdAsync(request.Id);

            if (staff == null)
            {
                throw new NotFoundException(nameof(Staff), request.Id);
            }

            return _mapper.Map<StaffDto>(staff);
        }
    }
}