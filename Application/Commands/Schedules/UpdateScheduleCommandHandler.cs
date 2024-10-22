using Application.Exceptions;
using AutoMapper;
using Domain.Entities;
using Domain.Repositories;
using MediatR;

namespace Application.Commands.Schedules
{
    public class UpdateScheduleCommandHandler : IRequestHandler<UpdateScheduleCommand, Unit>
    {
        private readonly IScheduleRepository _scheduleRepository;
        private readonly ISpecialtyRepository _specialtyRepository;
        private readonly IMapper _mapper;

        public UpdateScheduleCommandHandler(IScheduleRepository scheduleRepository, ISpecialtyRepository specialtyRepository, IMapper mapper)
        {
            _scheduleRepository = scheduleRepository;
            _specialtyRepository = specialtyRepository;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateScheduleCommand request, CancellationToken cancellationToken)
        {
            var schedule = await _scheduleRepository.GetByIdAsync(request.Id);
            if (schedule == null)
            {
                throw new NotFoundException(nameof(Schedule), request.ScheduleDto);
            }

            var specialty = await _specialtyRepository.GetByIdAsync(request.ScheduleDto.SpecialtyId);
            if (specialty == null)
            {
                throw new NotFoundException(nameof(Schedule), request.ScheduleDto);
            }

            schedule = _mapper.Map(request.ScheduleDto, schedule);
            schedule.Specialty = specialty;

            await _scheduleRepository.UpdateAsync(schedule);
            return Unit.Value;
        }
    }
}