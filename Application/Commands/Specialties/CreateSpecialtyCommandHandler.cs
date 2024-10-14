using Application.Models;
using AutoMapper;
using Domain.Entities;
using Domain.Repositories;
using MediatR;

namespace Application.Commands.Specialties
{
    /// <summary>
    /// Handler for creating a new Specialty.
    /// </summary>
    public class CreateSpecialtyCommandHandler : IRequestHandler<CreateSpecialtyCommand, int>
    {
        private readonly ISpecialtyRepository _specialtyRepository;
        private readonly IMapper _mapper;

        public CreateSpecialtyCommandHandler(ISpecialtyRepository specialtyRepository, IMapper mapper)
        {
            _specialtyRepository = specialtyRepository;
            _mapper = mapper;
        }

        public async Task<int> Handle(CreateSpecialtyCommand request, CancellationToken cancellationToken)
        {
            // Map SpecialtyDto to Specialty entity
            var specialty = _mapper.Map<Specialty>(request.SpecialtyDto);

            specialty.CreatedAt = DateTime.Now;
            specialty.UpdatedAt = DateTime.Now;

            await _specialtyRepository.AddAsync(specialty);
            return specialty.Id;
        }
    }
}