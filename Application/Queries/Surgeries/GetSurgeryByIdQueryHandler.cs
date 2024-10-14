using Application.Exceptions;
using Application.Models;
using AutoMapper;
using Domain.Entities;
using Domain.Repositories;
using MediatR;

namespace Application.Queries.Surgeries
{
    /// <summary>
    /// Handler for retrieving a Surgery by ID.
    /// </summary>
    public class GetSurgeryByIdQueryHandler : IRequestHandler<GetSurgeryByIdQuery, SurgeryDto>
    {
        private readonly ISurgeryRepository _surgeryRepository;
        private readonly IMapper _mapper;

        public GetSurgeryByIdQueryHandler(ISurgeryRepository surgeryRepository, IMapper mapper)
        {
            _surgeryRepository = surgeryRepository;
            _mapper = mapper;
        }

        public async Task<SurgeryDto> Handle(GetSurgeryByIdQuery request, CancellationToken cancellationToken)
        {
            var surgery = await _surgeryRepository.GetByIdAsync(request.Id);
            if (surgery == null)
            {
                throw new NotFoundException(nameof(Surgery), request.Id);
            }

            return _mapper.Map<SurgeryDto>(surgery);  // Map entity to DTO
        }
    }
}