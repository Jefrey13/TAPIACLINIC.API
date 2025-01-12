using Application.Exceptions;
using Application.Models.ReponseDtos;
using AutoMapper;
using Domain.Repositories;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Queries.MedicalRecords
{
    /// <summary>
    /// Handler for GetMedicalRecordsByPatientIdQuery.
    /// Retrieves medical records based on the patient's ID.
    /// </summary>
    public class GetMedicalRecordsByPatientIdQueryHandler : IRequestHandler<GetMedicalRecordsByPatientIdQuery, List<MedicalRecordResponseDto>>
    {
        private readonly IMedicalRecordRepository _repository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the GetMedicalRecordsByPatientIdQueryHandler class.
        /// </summary>
        /// <param name="repository">The repository to handle the query.</param>
        /// <param name="mapper">Mapper to convert the entities to DTOs.</param>
        public GetMedicalRecordsByPatientIdQueryHandler(IMedicalRecordRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        /// <summary>
        /// Handles the query to retrieve medical records by patient ID.
        /// </summary>
        /// <param name="request">The query containing the patient's ID.</param>
        /// <param name="cancellationToken">Cancellation token for the request.</param>
        /// <returns>A list of MedicalRecordResponseDto containing the details of the medical records.</returns>
        public async Task<List<MedicalRecordResponseDto>> Handle(GetMedicalRecordsByPatientIdQuery request, CancellationToken cancellationToken)
        {
            var medicalRecords = await _repository.GetMedicalRecordByPatientIdAsync(request.PatientId);

            if (!medicalRecords.Any())
            {
                throw new NotFoundException(nameof(medicalRecords), request.PatientId);
            }

            return _mapper.Map<List<MedicalRecordResponseDto>>(medicalRecords);
        }
    }
}