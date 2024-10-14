using Application.Models;
using AutoMapper;
using Domain.Repositories;
using MediatR;

namespace Application.Queries.Specialties
{
    /// <summary>
    /// Handler for retrieving all Specialties.
    /// </summary>
    public class GetAllSpecialtiesQueryHandler : IRequestHandler<GetAllSpecialtiesQuery, IEnumerable<SpecialtyDto>>
    {
        private readonly ISpecialtyRepository _specialtyRepository;
        private readonly IMapper _mapper;

        public GetAllSpecialtiesQueryHandler(ISpecialtyRepository specialtyRepository, IMapper mapper)
        {
            _specialtyRepository = specialtyRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<SpecialtyDto>> Handle(GetAllSpecialtiesQuery request, CancellationToken cancellationToken)
        {
            var specialties = await _specialtyRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<SpecialtyDto>>(specialties);  // Map entities to DTOs
        }
    }
}