using Application.Exceptions;
using Application.Models;
using AutoMapper;
using Domain.Entities;
using Domain.Repositories;
using MediatR;

namespace Application.Queries.Specialties
{
    /// <summary>
    /// Handler for retrieving a Specialty by ID.
    /// </summary>
    public class GetSpecialtyByIdQueryHandler : IRequestHandler<GetSpecialtyByIdQuery, SpecialtyDto>
    {
        private readonly ISpecialtyRepository _specialtyRepository;
        private readonly IMapper _mapper;

        public GetSpecialtyByIdQueryHandler(ISpecialtyRepository specialtyRepository, IMapper mapper)
        {
            _specialtyRepository = specialtyRepository;
            _mapper = mapper;
        }

        public async Task<SpecialtyDto> Handle(GetSpecialtyByIdQuery request, CancellationToken cancellationToken)
        {
            var specialty = await _specialtyRepository.GetByIdAsync(request.Id);
            if (specialty == null)
            {
                throw new NotFoundException(nameof(Specialty), request.Id);
            }

            return _mapper.Map<SpecialtyDto>(specialty);  // Map entity to DTO
        }
    }
}