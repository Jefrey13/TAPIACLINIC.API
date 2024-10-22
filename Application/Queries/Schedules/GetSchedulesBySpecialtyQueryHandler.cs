using Application.Models.ReponseDtos;
using AutoMapper;
using Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.Schedules
{
    public class GetSchedulesBySpecialtyQueryHandler : IRequestHandler<GetSchedulesBySpecialtyQuery, IEnumerable<ScheduleResponseDto>>
    {
        private readonly IScheduleRepository _scheduleRepository;
        private readonly IMapper _mapper;

        public GetSchedulesBySpecialtyQueryHandler(IScheduleRepository scheduleRepository, IMapper mapper)
        {
            _scheduleRepository = scheduleRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ScheduleResponseDto>> Handle(GetSchedulesBySpecialtyQuery request, CancellationToken cancellationToken)
        {
            var schedules = await _scheduleRepository.GetSchedulesBySpecialtyAsync(request.SpecialtyId);
            return _mapper.Map<IEnumerable<ScheduleResponseDto>>(schedules);
        }
    }
}
