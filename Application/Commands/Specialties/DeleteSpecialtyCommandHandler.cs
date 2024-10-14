using Application.Exceptions;
using Domain.Entities;
using Domain.Repositories;
using MediatR;

namespace Application.Commands.Specialties
{
    /// <summary>
    /// Handler for deleting a Specialty by ID.
    /// </summary>
    public class DeleteSpecialtyCommandHandler : IRequestHandler<DeleteSpecialtyCommand, Unit>
    {
        private readonly ISpecialtyRepository _specialtyRepository;

        public DeleteSpecialtyCommandHandler(ISpecialtyRepository specialtyRepository)
        {
            _specialtyRepository = specialtyRepository;
        }

        public async Task<Unit> Handle(DeleteSpecialtyCommand request, CancellationToken cancellationToken)
        {
            var specialty = await _specialtyRepository.GetByIdAsync(request.Id);
            if (specialty == null)
            {
                throw new NotFoundException(nameof(Specialty), request.Id);
            }

            await _specialtyRepository.DeleteAsync(specialty);
            return Unit.Value;
        }
    }
}