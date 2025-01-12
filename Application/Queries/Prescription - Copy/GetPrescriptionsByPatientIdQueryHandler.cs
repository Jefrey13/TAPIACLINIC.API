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
    /// Handler for GetPrescriptionsByPatientIdQuery.
    /// Retrieves prescriptions by patient ID.
    /// </summary>
    public class GetPrescriptionsByPatientIdQueryHandler : IRequestHandler<GetPrescriptionsByPatientIdQuery, IEnumerable<PrescriptionResponseDto>>
    {
        private readonly IPrescriptionRepository _repository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the GetPrescriptionsByPatientIdQueryHandler class.
        /// </summary>
        /// <param name="repository">The repository to handle the query.</param>
        /// <param name="mapper">Mapper to convert entity to DTO.</param>
        public GetPrescriptionsByPatientIdQueryHandler(IPrescriptionRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        /// <summary>
        /// Handles the query to retrieve prescriptions by patient ID.
        /// </summary>
        /// <param name="request">The query containing the patient ID.</param>
        /// <param name="cancellationToken">Cancellation token for the request.</param>
        /// <returns>An IEnumerable of PrescriptionResponseDto containing the prescriptions for the specified patient.</returns>
        public async Task<IEnumerable<PrescriptionResponseDto>> Handle(GetPrescriptionsByPatientIdQuery request, CancellationToken cancellationToken)
        {
            var prescriptions = await _repository.GetPrescriptionsByPatientIdAsync(request.PatientId);
            return _mapper.Map<IEnumerable<PrescriptionResponseDto>>(prescriptions);
        }
    }
}