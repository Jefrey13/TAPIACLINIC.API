using Application.Models;
using AutoMapper;
using Domain.Repositories;
using MediatR;

namespace Application.Queries.Surgeries
{
    /// <summary>
    /// Handler for retrieving all Surgeries.
    /// </summary>
    public class GetAllSurgeriesQueryHandler : IRequestHandler<GetAllSurgeriesQuery, IEnumerable<SurgeryDto>>
    {
        private readonly ISurgeryRepository _surgeryRepository;
        private readonly IMapper _mapper;

        public GetAllSurgeriesQueryHandler(ISurgeryRepository surgeryRepository, IMapper mapper)
        {
            _surgeryRepository = surgeryRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<SurgeryDto>> Handle(GetAllSurgeriesQuery request, CancellationToken cancellationToken)
        {
            var surgeries = await _surgeryRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<SurgeryDto>>(surgeries);  // Map entities to DTOs
        }
    }
}