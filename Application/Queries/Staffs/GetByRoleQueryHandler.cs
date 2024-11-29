using Application.Exceptions;
using Application.Models.ReponseDtos;
using AutoMapper;
using Domain.Entities;
using Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.Staffs
{
    public class GetByRoleQueryHandler : IRequestHandler<GetByRoleQuery, IEnumerable<StaffResponseDto>>
    {
        private readonly IStaffRepository _staffRepository;
        private readonly IMapper _mapper;

        public GetByRoleQueryHandler(IStaffRepository staffRepository, IMapper mapper)
        {
            _staffRepository = staffRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<StaffResponseDto>> Handle(GetByRoleQuery request, CancellationToken cancellationToken)
        {
            var staffs = await _staffRepository.GetByRoleAsync(request.RoleName);

            if (staffs == null)
            {
                throw new NotFoundException(nameof(Staff), request.RoleName);
            }

            // Map the staff member to a response DTO and return it
            return _mapper.Map<IEnumerable<StaffResponseDto>>(staffs); // Map the result to DTOs
        }
    }
}