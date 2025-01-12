using Application.Models.ResponseDtos;
using Application.Queries.Prescriptions;
using AutoMapper;
using Domain.Repositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Handlers.Prescriptions
{
    /// <summary>
    /// Handler for GetPrescriptionByIdQuery.
    /// Retrieves a prescription by its ID.
    /// </summary>
    public class GetPrescriptionByIdQueryHandler : IRequestHandler<GetPrescriptionByIdQuery, PrescriptionResponseDto>
    {
        private readonly IPrescriptionRepository _repository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the GetPrescriptionByIdQueryHandler class.
        /// </summary>
        /// <param name="repository">The repository to handle the query.</param>
        /// <param name="mapper">Mapper to convert entity to DTO.</param>
        public GetPrescriptionByIdQueryHandler(IPrescriptionRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        /// <summary>
        /// Handles the query to retrieve a prescription by its ID.
        /// </summary>
        /// <param name="request">The query to retrieve the prescription.</param>
        /// <param name="cancellationToken">Cancellation token for the request.</param>
        /// <returns>A PrescriptionResponseDto containing the prescription details.</returns>
        public async Task<PrescriptionResponseDto> Handle(GetPrescriptionByIdQuery request, CancellationToken cancellationToken)
        {
            var prescription = await _repository.GetByIdAsync(request.Id);
            return _mapper.Map<PrescriptionResponseDto>(prescription);
        }
    }
}