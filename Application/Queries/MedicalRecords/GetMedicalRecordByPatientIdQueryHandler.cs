using Application.Exceptions;
using Application.Models.ReponseDtos;
using AutoMapper;
using Domain.Entities;
using Domain.Repositories;
using MediatR;

namespace Application.Queries.MedicalRecords
{
    /// <summary>
    /// Handler for GetMedicalRecordByPatientIdQuery.
    /// Retrieves a medical record based on the patient's ID.
    /// </summary>
    public class GetMedicalRecordByPatientIdQueryHandler : IRequestHandler<GetMedicalRecordByPatientIdQuery, MedicalRecordResponseDto>
    {
        private readonly IMedicalRecordRepository _repository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the GetMedicalRecordByPatientIdQueryHandler class.
        /// </summary>
        /// <param name="repository">The repository to handle the query.</param>
        /// <param name="mapper">Mapper to convert the entity to a DTO.</param>
        public GetMedicalRecordByPatientIdQueryHandler(IMedicalRecordRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        /// <summary>
        /// Handles the query to retrieve a medical record by patient ID.
        /// </summary>
        /// <param name="request">The query containing the patient's ID.</param>
        /// <param name="cancellationToken">Cancellation token for the request.</param>
        /// <returns>A MedicalRecordResponseDto containing the details of the medical record.</returns>
        public async Task<MedicalRecordResponseDto> Handle(GetMedicalRecordByPatientIdQuery request, CancellationToken cancellationToken)
        {
            var medicalRecord = await _repository.GetMedicalRecordByPatientIdAsync(request.PatientId);
            if (medicalRecord == null)
            {
                throw new NotFoundException(nameof(MedicalRecord), request.PatientId);
            }

            return _mapper.Map<MedicalRecordResponseDto>(medicalRecord);
        }
    }
}
