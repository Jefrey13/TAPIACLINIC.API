using Application.Commands.MedicalRecords;
using Application.Exceptions;
using AutoMapper;
using Domain.Entities;
using Domain.Repositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Commands.MedicalRecords
{
    /// <summary>
    /// Handler for UpdateMedicalRecordCommand.
    /// Handles the update of an existing medical record.
    /// </summary>
    public class UpdateMedicalRecordCommandHandler : IRequestHandler<UpdateMedicalRecordCommand, Unit>
    {
        private readonly IMedicalRecordRepository _repository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the UpdateMedicalRecordCommandHandler class.
        /// </summary>
        /// <param name="repository">The repository to handle the update.</param>
        /// <param name="mapper">Mapper to convert DTO to entity.</param>
        public UpdateMedicalRecordCommandHandler(IMedicalRecordRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        /// <summary>
        /// Handles the update of the medical record.
        /// </summary>
        /// <param name="request">The command containing the updated medical record details.</param>
        /// <param name="cancellationToken">Cancellation token for the request.</param>
        /// <returns>A Unit indicating the completion of the task.</returns>
        public async Task<Unit> Handle(UpdateMedicalRecordCommand request, CancellationToken cancellationToken)
        {
            var medicalRecord = await _repository.GetByIdAsync(request.Id);
            if (medicalRecord == null)
            {
                throw new NotFoundException(nameof(MedicalRecord), request.Id);
            }

            _mapper.Map(request.MedicalRecordDto, medicalRecord);
            await _repository.UpdateAsync(medicalRecord);

            return Unit.Value;  // Return Unit indicating successful completion
        }
    }
}