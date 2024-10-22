using Application.Exceptions;
using AutoMapper;
using Domain.Entities;
using Domain.Repositories;
using MediatR;

namespace Application.Commands.Schedules
{
    public class CreateScheduleCommandHandler : IRequestHandler<CreateScheduleCommand, int>
    {
        private readonly IScheduleRepository _scheduleRepository;
        private readonly ISpecialtyRepository _specialtyRepository;
        private readonly IMapper _mapper;

        public CreateScheduleCommandHandler(IScheduleRepository scheduleRepository, ISpecialtyRepository specialtyRepository, IMapper mapper)
        {
            _scheduleRepository = scheduleRepository;
            _specialtyRepository = specialtyRepository;
            _mapper = mapper;
        }

        public async Task<int> Handle(CreateScheduleCommand request, CancellationToken cancellationToken)
        {
            var specialty = await _specialtyRepository.GetByIdAsync(request.ScheduleDto.SpecialtyId);
            if (specialty == null)
            {
                throw new NotFoundException(nameof(Schedule), request.ScheduleDto);
            }

            var schedule = _mapper.Map<Schedule>(request.ScheduleDto);
            schedule.Specialty = specialty;

            await _scheduleRepository.AddAsync(schedule);
            return schedule.Id;
        }
    }
}
