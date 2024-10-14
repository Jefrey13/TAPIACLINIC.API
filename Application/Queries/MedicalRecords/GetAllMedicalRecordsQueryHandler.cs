using Application.Models;
using Application.Queries.MedicalRecords;
using AutoMapper;
using Domain.Repositories;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Handlers.MedicalRecords
{
    public class GetAllMedicalRecordsQueryHandler : IRequestHandler<GetAllMedicalRecordsQuery, IEnumerable<MedicalRecordDto>>
    {
        private readonly IMedicalRecordRepository _medicalRecordRepository;
        private readonly IMapper _mapper;

        public GetAllMedicalRecordsQueryHandler(IMedicalRecordRepository medicalRecordRepository, IMapper mapper)
        {
            _medicalRecordRepository = medicalRecordRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<MedicalRecordDto>> Handle(GetAllMedicalRecordsQuery request, CancellationToken cancellationToken)
        {
            var records = await _medicalRecordRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<MedicalRecordDto>>(records);
        }
    }
}