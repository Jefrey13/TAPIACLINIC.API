using Application.Commands.MedicalRecords;
using Application.Exceptions;
using Domain.Entities;
using Domain.Repositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Handlers.MedicalRecords
{
    public class DeleteMedicalRecordCommandHandler : IRequestHandler<DeleteMedicalRecordCommand, Unit>
    {
        private readonly IMedicalRecordRepository _medicalRecordRepository;

        public DeleteMedicalRecordCommandHandler(IMedicalRecordRepository medicalRecordRepository)
        {
            _medicalRecordRepository = medicalRecordRepository;
        }

        public async Task<Unit> Handle(DeleteMedicalRecordCommand request, CancellationToken cancellationToken)
        {
            var record = await _medicalRecordRepository.GetByIdAsync(request.Id);
            if (record == null)
            {
                throw new NotFoundException(nameof(MedicalRecord), request.Id);
            }

            await _medicalRecordRepository.DeleteAsync(record);

            return Unit.Value;
        }
    }
}