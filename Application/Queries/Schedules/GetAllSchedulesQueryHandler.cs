using Application.Models.ReponseDtos;
using AutoMapper;
using Domain.Repositories;
using MediatR;

namespace Application.Queries.Schedules
{
    public class GetAllSchedulesQueryHandler : IRequestHandler<GetAllSchedulesQuery, IEnumerable<ScheduleResponseDto>>
    {
        private readonly IScheduleRepository _scheduleRepository;
        private readonly IMapper _mapper;

        public GetAllSchedulesQueryHandler(IScheduleRepository scheduleRepository, IMapper mapper)
        {
            _scheduleRepository = scheduleRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ScheduleResponseDto>> Handle(GetAllSchedulesQuery request, CancellationToken cancellationToken)
        {
            var schedules = await _scheduleRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<ScheduleResponseDto>>(schedules);
        }
    }
}