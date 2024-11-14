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
    public class GetByStateQueryHandler: IRequestHandler<GetByStateQuery, IEnumerable<StaffResponseDto>>
    {
        private readonly IStaffRepository _staffRepository;
        private readonly IMapper _mapper;

        public GetByStateQueryHandler(IStaffRepository staffRepository, IMapper mapper)
        {
            _staffRepository = staffRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<StaffResponseDto>> Handle(GetByStateQuery request, CancellationToken cancellationToken)
        {
            var staffs = await _staffRepository.GetByStateAsync(request.StateId);

            if (staffs == null)
            {
                throw new NotFoundException(nameof(Staff), request.StateId);
            }

            // Map the staff member to a response DTO and return it
            return _mapper.Map<IEnumerable<StaffResponseDto>>(staffs); // Map the result to DTOs
        }
    }
}
