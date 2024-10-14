using Application.Models;
using AutoMapper;
using Domain.Entities;
using Domain.Repositories;
using MediatR;

namespace Application.Commands.Schedules
{
    /// <summary>
    /// Handler for creating a new schedule.
    /// Uses AutoMapper to map from ScheduleDto to the Schedule entity.
    /// </summary>
    public class CreateScheduleCommandHandler : IRequestHandler<CreateScheduleCommand, int>
    {
        private readonly IScheduleRepository _scheduleRepository;
        private readonly IMapper _mapper;

        public CreateScheduleCommandHandler(IScheduleRepository scheduleRepository, IMapper mapper)
        {
            _scheduleRepository = scheduleRepository;
            _mapper = mapper;
        }

        public async Task<int> Handle(CreateScheduleCommand request, CancellationToken cancellationToken)
        {
            // Map ScheduleDto to Schedule entity
            var schedule = _mapper.Map<Schedule>(request.ScheduleDto);

            // Set creation and update timestamps
            schedule.CreatedAt = DateTime.Now;
            schedule.UpdatedAt = DateTime.Now;

            await _scheduleRepository.AddAsync(schedule);
            return schedule.Id;
        }
    }
}
