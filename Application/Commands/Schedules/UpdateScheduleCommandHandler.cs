using Application.Exceptions;
using AutoMapper;
using Domain.Entities;
using Domain.Repositories;
using MediatR;

namespace Application.Commands.Schedules
{
    /// <summary>
    /// Handler for updating a schedule.
    /// Uses AutoMapper to map from ScheduleDto to the Schedule entity.
    /// </summary>
    public class UpdateScheduleCommandHandler : IRequestHandler<UpdateScheduleCommand, Unit>
    {
        private readonly IScheduleRepository _scheduleRepository;
        private readonly IMapper _mapper;

        public UpdateScheduleCommandHandler(IScheduleRepository scheduleRepository, IMapper mapper)
        {
            _scheduleRepository = scheduleRepository;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateScheduleCommand request, CancellationToken cancellationToken)
        {
            var schedule = await _scheduleRepository.GetByIdAsync(request.Id);
            if (schedule == null)
            {
                throw new NotFoundException(nameof(Schedule), request.Id);
            }

            // Map ScheduleDto to existing Schedule entity
            _mapper.Map(request.ScheduleDto, schedule);

            // Update timestamp
            schedule.UpdatedAt = DateTime.Now;

            await _scheduleRepository.UpdateAsync(schedule);
            return Unit.Value;
        }
    }
}