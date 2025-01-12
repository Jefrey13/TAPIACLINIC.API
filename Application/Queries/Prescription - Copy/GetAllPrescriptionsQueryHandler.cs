using Application.Models.ResponseDtos;
using Application.Queries.Prescriptions;
using AutoMapper;
using Domain.Repositories;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Handlers.Prescriptions
{
    /// <summary>
    /// Handler for GetAllPrescriptionsQuery.
    /// Retrieves all prescriptions from the system.
    /// </summary>
    public class GetAllPrescriptionsQueryHandler : IRequestHandler<GetAllPrescriptionsQuery, IEnumerable<PrescriptionResponseDto>>
    {
        private readonly IPrescriptionRepository _repository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the GetAllPrescriptionsQueryHandler class.
        /// </summary>
        /// <param name="repository">The repository to handle the query.</param>
        /// <param name="mapper">Mapper to convert entity to DTO.</param>
        public GetAllPrescriptionsQueryHandler(IPrescriptionRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        /// <summary>
        /// Handles the query to retrieve all prescriptions.
        /// </summary>
        /// <param name="request">The query to retrieve all prescriptions.</param>
        /// <param name="cancellationToken">Cancellation token for the request.</param>
        /// <returns>A list of PrescriptionResponseDto representing all prescriptions.</returns>
        public async Task<IEnumerable<PrescriptionResponseDto>> Handle(GetAllPrescriptionsQuery request, CancellationToken cancellationToken)
        {
            var prescriptions = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<PrescriptionResponseDto>>(prescriptions);
        }
    }
}
