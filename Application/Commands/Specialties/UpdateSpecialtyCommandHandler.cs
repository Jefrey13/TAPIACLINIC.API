using Application.Exceptions;
using AutoMapper;
using Domain.Entities;
using Domain.Repositories;
using MediatR;

namespace Application.Commands.Specialties
{
    /// <summary>
    /// Handler for updating a Specialty.
    /// </summary>
    public class UpdateSpecialtyCommandHandler : IRequestHandler<UpdateSpecialtyCommand, Unit>
    {
        private readonly ISpecialtyRepository _specialtyRepository;
        private readonly IMapper _mapper;

        public UpdateSpecialtyCommandHandler(ISpecialtyRepository specialtyRepository, IMapper mapper)
        {
            _specialtyRepository = specialtyRepository;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateSpecialtyCommand request, CancellationToken cancellationToken)
        {
            var specialty = await _specialtyRepository.GetByIdAsync(request.Id);
            if (specialty == null)
            {
                throw new NotFoundException(nameof(Specialty), request.Id);
            }

            // Map SpecialtyDto to the existing Specialty entity
            _mapper.Map(request.SpecialtyDto, specialty);

            specialty.UpdatedAt = DateTime.Now;

            await _specialtyRepository.UpdateAsync(specialty);
            return Unit.Value;
        }
    }
}