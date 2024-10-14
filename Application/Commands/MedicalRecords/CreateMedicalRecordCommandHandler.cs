using Application.Commands.MedicalRecords;
using AutoMapper;
using Domain.Entities;
using Domain.Repositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Handlers.MedicalRecords
{
    public class CreateMedicalRecordCommandHandler : IRequestHandler<CreateMedicalRecordCommand, int>
    {
        private readonly IMedicalRecordRepository _medicalRecordRepository;
        private readonly IMapper _mapper;

        public CreateMedicalRecordCommandHandler(IMedicalRecordRepository medicalRecordRepository, IMapper mapper)
        {
            _medicalRecordRepository = medicalRecordRepository;
            _mapper = mapper;
        }

        public async Task<int> Handle(CreateMedicalRecordCommand request, CancellationToken cancellationToken)
        {
            var medicalRecord = _mapper.Map<MedicalRecord>(request.MedicalRecordDto);
            await _medicalRecordRepository.AddAsync(medicalRecord);
            return medicalRecord.Id;
        }
    }
}