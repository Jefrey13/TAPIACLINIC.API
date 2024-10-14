using Application.Exceptions;
using Domain.Entities;
using Domain.Repositories;
using MediatR;

namespace Application.Commands.Surgeries
{
    /// <summary>
    /// Handler for deleting a Surgery by ID.
    /// </summary>
    public class DeleteSurgeryCommandHandler : IRequestHandler<DeleteSurgeryCommand, Unit>
    {
        private readonly ISurgeryRepository _surgeryRepository;

        public DeleteSurgeryCommandHandler(ISurgeryRepository surgeryRepository)
        {
            _surgeryRepository = surgeryRepository;
        }

        public async Task<Unit> Handle(DeleteSurgeryCommand request, CancellationToken cancellationToken)
        {
            var surgery = await _surgeryRepository.GetByIdAsync(request.Id);
            if (surgery == null)
            {
                throw new NotFoundException(nameof(Surgery), request.Id);
            }

            await _surgeryRepository.DeleteAsync(surgery);
            return Unit.Value;
        }
    }
}