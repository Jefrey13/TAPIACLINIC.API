using Application.Exceptions;
using Application.Models.ReponseDtos;
using AutoMapper;
using Domain.Entities;
using Domain.Repositories;
using MediatR;

namespace Application.Queries.Schedules
{
    public class GetScheduleByIdQueryHandler : IRequestHandler<GetScheduleByIdQuery, ScheduleResponseDto>
    {
        private readonly IScheduleRepository _scheduleRepository;
        private readonly IMapper _mapper;

        public GetScheduleByIdQueryHandler(IScheduleRepository scheduleRepository, IMapper mapper)
        {
            _scheduleRepository = scheduleRepository;
            _mapper = mapper;
        }

        public async Task<ScheduleResponseDto> Handle(GetScheduleByIdQuery request, CancellationToken cancellationToken)
        {
            var schedule = await _scheduleRepository.GetByIdAsync(request.Id);
            if (schedule == null)
            {
                throw new NotFoundException(nameof(Schedule), request.Id);
            }

            return _mapper.Map<ScheduleResponseDto>(schedule);
        }
    }
}