using Application.Models;
using Application.Models.ReponseDtos;
using Application.Queries.MedicalRecords;
using AutoMapper;
using Domain.Repositories;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Handlers.MedicalRecords
{
    /// <summary>
    /// Handler for GetAllMedicalRecordsQuery.
    /// Retrieves all medical records.
    /// </summary>
    public class GetAllMedicalRecordsQueryHandler : IRequestHandler<GetAllMedicalRecordsQuery, IEnumerable<MedicalRecordResponseDto>>
    {
        private readonly IMedicalRecordRepository _repository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the GetAllMedicalRecordsQueryHandler class.
        /// </summary>
        /// <param name="repository">The repository to handle the query.</param>
        /// <param name="mapper">Mapper to convert entity to DTO.</param>
        public GetAllMedicalRecordsQueryHandler(IMedicalRecordRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        /// <summary>
        /// Handles the query to retrieve all medical records.
        /// </summary>
        /// <param name="request">The query to retrieve all medical records.</param>
        /// <param name="cancellationToken">Cancellation token for the request.</param>
        /// <returns>An IEnumerable of MedicalRecordResponseDto containing all medical records.</returns>
        public async Task<IEnumerable<MedicalRecordResponseDto>> Handle(GetAllMedicalRecordsQuery request, CancellationToken cancellationToken)
        {
            var medicalRecords = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<MedicalRecordResponseDto>>(medicalRecords);
        }
    }
}