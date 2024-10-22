using Application.Exceptions;
using Application.Models;
using Application.Models.ReponseDtos;
using Application.Queries.MedicalRecords;
using AutoMapper;
using Domain.Entities;
using Domain.Repositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Handlers.MedicalRecords
{
    /// <summary>
    /// Handler for GetMedicalRecordByIdQuery.
    /// Retrieves a medical record by its ID.
    /// </summary>
    public class GetMedicalRecordByIdQueryHandler : IRequestHandler<GetMedicalRecordByIdQuery, MedicalRecordResponseDto>
    {
        private readonly IMedicalRecordRepository _repository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the GetMedicalRecordByIdQueryHandler class.
        /// </summary>
        /// <param name="repository">The repository to handle the query.</param>
        /// <param name="mapper">Mapper to convert entity to DTO.</param>
        public GetMedicalRecordByIdQueryHandler(IMedicalRecordRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        /// <summary>
        /// Handles the query to retrieve a medical record by its ID.
        /// </summary>
        /// <param name="request">The query containing the ID of the medical record.</param>
        /// <param name="cancellationToken">Cancellation token for the request.</param>
        /// <returns>A MedicalRecordResponseDto containing the details of the medical record.</returns>
        public async Task<MedicalRecordResponseDto> Handle(GetMedicalRecordByIdQuery request, CancellationToken cancellationToken)
        {
            var medicalRecord = await _repository.GetByIdAsync(request.Id);
            if (medicalRecord == null)
            {
                throw new NotFoundException(nameof(MedicalRecord), request.Id);
            }

            return _mapper.Map<MedicalRecordResponseDto>(medicalRecord);
        }
    }
}