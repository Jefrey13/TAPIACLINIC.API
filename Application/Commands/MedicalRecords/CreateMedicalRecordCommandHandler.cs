using Application.Commands.MedicalRecords;
using AutoMapper;
using Domain.Entities;
using Domain.Repositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Handlers.MedicalRecords
{
    /// <summary>
    /// Handler for CreateMedicalRecordCommand.
    /// Handles the creation of a new medical record.
    /// </summary>
    public class CreateMedicalRecordCommandHandler : IRequestHandler<CreateMedicalRecordCommand, int>
    {
        private readonly IMedicalRecordRepository _repository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the CreateMedicalRecordCommandHandler class.
        /// </summary>
        /// <param name="repository">The repository to handle the creation.</param>
        /// <param name="mapper">Mapper to convert DTO to entity.</param>
        public CreateMedicalRecordCommandHandler(IMedicalRecordRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        /// <summary>
        /// Handles the creation of a medical record.
        /// </summary>
        /// <param name="request">The command containing the medical record details.</param>
        /// <param name="cancellationToken">Cancellation token for the request.</param>
        /// <returns>The ID of the newly created medical record.</returns>
        public async Task<int> Handle(CreateMedicalRecordCommand request, CancellationToken cancellationToken)
        {
            var medicalRecord = _mapper.Map<MedicalRecord>(request.MedicalRecordDto);
            await _repository.AddAsync(medicalRecord);
            return medicalRecord.Id;
        }
    }
}