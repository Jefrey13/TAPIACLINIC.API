using Application.Exceptions;
using Domain.Entities;
using Domain.Repositories;
using MediatR;

namespace Application.Commands.Schedules
{
    /// <summary>
    /// Handler for deleting a schedule by ID.
    /// Throws NotFoundException if the schedule doesn't exist.
    /// </summary>
    public class DeleteScheduleCommandHandler : IRequestHandler<DeleteScheduleCommand, Unit>
    {
        private readonly IScheduleRepository _scheduleRepository;

        public DeleteScheduleCommandHandler(IScheduleRepository scheduleRepository)
        {
            _scheduleRepository = scheduleRepository;
        }

        public async Task<Unit> Handle(DeleteScheduleCommand request, CancellationToken cancellationToken)
        {
            var schedule = await _scheduleRepository.GetByIdAsync(request.Id);

            if (schedule == null)
            {
                throw new NotFoundException(nameof(Schedule), request.Id);
            }

            await _scheduleRepository.DeleteAsync(schedule);

            return Unit.Value;
        }
    }
}