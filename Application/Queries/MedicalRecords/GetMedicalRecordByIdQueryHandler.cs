using Application.Exceptions;
using Application.Models;
using Application.Queries.MedicalRecords;
using AutoMapper;
using Domain.Entities;
using Domain.Repositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Handlers.MedicalRecords
{
    public class GetMedicalRecordByIdQueryHandler : IRequestHandler<GetMedicalRecordByIdQuery, MedicalRecordDto>
    {
        private readonly IMedicalRecordRepository _medicalRecordRepository;
        private readonly IMapper _mapper;

        public GetMedicalRecordByIdQueryHandler(IMedicalRecordRepository medicalRecordRepository, IMapper mapper)
        {
            _medicalRecordRepository = medicalRecordRepository;
            _mapper = mapper;
        }

        public async Task<MedicalRecordDto> Handle(GetMedicalRecordByIdQuery request, CancellationToken cancellationToken)
        {
            var record = await _medicalRecordRepository.GetByIdAsync(request.Id);
            if (record == null)
            {
                throw new NotFoundException(nameof(MedicalRecord), request.Id);
            }

            return _mapper.Map<MedicalRecordDto>(record);
        }
    }
}