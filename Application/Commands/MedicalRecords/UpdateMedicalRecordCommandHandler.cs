using Application.Commands.MedicalRecords;
using Application.Exceptions;
using AutoMapper;
using Domain.Entities;
using Domain.Repositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Handlers.MedicalRecords
{
    public class UpdateMedicalRecordCommandHandler : IRequestHandler<UpdateMedicalRecordCommand, Unit>
    {
        private readonly IMedicalRecordRepository _medicalRecordRepository;
        private readonly IMapper _mapper;

        public UpdateMedicalRecordCommandHandler(IMedicalRecordRepository medicalRecordRepository, IMapper mapper)
        {
            _medicalRecordRepository = medicalRecordRepository;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateMedicalRecordCommand request, CancellationToken cancellationToken)
        {
            var existingRecord = await _medicalRecordRepository.GetByIdAsync(request.Id);
            if (existingRecord == null)
            {
                throw new NotFoundException(nameof(MedicalRecord), request.Id);
            }

            _mapper.Map(request.MedicalRecordDto, existingRecord);
            await _medicalRecordRepository.UpdateAsync(existingRecord);

            return Unit.Value;
        }
    }
}