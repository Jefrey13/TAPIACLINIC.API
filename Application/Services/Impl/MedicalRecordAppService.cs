using Application.Commands.MedicalRecords;
using Application.Models;
using Application.Queries.MedicalRecords;
using AutoMapper;
using MediatR;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Services.Impl
{
    public class MedicalRecordAppService : IMedicalRecordAppService
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public MedicalRecordAppService(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        public async Task<int> CreateMedicalRecordAsync(CreateMedicalRecordCommand command)
        {
            return await _mediator.Send(command);
        }

        public async Task UpdateMedicalRecordAsync(UpdateMedicalRecordCommand command)
        {
            await _mediator.Send(command);
        }

        public async Task DeleteMedicalRecordAsync(int id)
        {
            await _mediator.Send(new DeleteMedicalRecordCommand(id));
        }

        public async Task<IEnumerable<MedicalRecordDto>> GetAllMedicalRecordsAsync()
        {
            return await _mediator.Send(new GetAllMedicalRecordsQuery());
        }

        public async Task<MedicalRecordDto> GetMedicalRecordByIdAsync(int id)
        {
            return await _mediator.Send(new GetMedicalRecordByIdQuery(id));
        }
    }
}