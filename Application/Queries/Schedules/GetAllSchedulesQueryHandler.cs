using Application.Models.ReponseDtos;
using AutoMapper;
using Domain.Repositories;
using MediatR;

namespace Application.Queries.Schedules
{
    /// <summary>
    /// Handler for retrieving all schedules.
    /// Maps from Schedule entities to ScheduleDto.
    /// </summary>
    public class GetAllSchedulesQueryHandler : IRequestHandler<GetAllSchedulesQuery, IEnumerable<ScheduleDto>>
    {
        private readonly IScheduleRepository _scheduleRepository;
        private readonly IMapper _mapper;

        public GetAllSchedulesQueryHandler(IScheduleRepository scheduleRepository, IMapper mapper)
        {
            _scheduleRepository = scheduleRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ScheduleDto>> Handle(GetAllSchedulesQuery request, CancellationToken cancellationToken)
        {
            var schedules = await _scheduleRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<ScheduleDto>>(schedules);  // Map entities to DTOs
        }
    }
}