using Application.Exceptions;
using Domain.Entities;
using Domain.Repositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Commands.MedicalRecords
{
    /// <summary>
    /// Handler for DeleteMedicalRecordCommand.
    /// Deletes a medical record based on its ID.
    /// </summary>
    public class DeleteMedicalRecordCommandHandler : IRequestHandler<DeleteMedicalRecordCommand, Unit>
    {
        private readonly IMedicalRecordRepository _repository;

        /// <summary>
        /// Initializes a new instance of the DeleteMedicalRecordCommandHandler class.
        /// </summary>
        /// <param name="repository">The repository to handle the deletion.</param>
        public DeleteMedicalRecordCommandHandler(IMedicalRecordRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Handles the deletion of the medical record.
        /// </summary>
        /// <param name="request">The command containing the ID of the medical record to delete.</param>
        /// <param name="cancellationToken">Cancellation token for the request.</param>
        /// <returns>A Unit indicating the completion of the task.</returns>
        public async Task<Unit> Handle(DeleteMedicalRecordCommand request, CancellationToken cancellationToken)
        {
            var medicalRecord = await _repository.GetByIdAsync(request.Id);
            if (medicalRecord == null)
            {
                throw new NotFoundException(nameof(MedicalRecord), request.Id);
            }

            await _repository.DeleteAsync(medicalRecord);
            return Unit.Value;
        }
    }
}