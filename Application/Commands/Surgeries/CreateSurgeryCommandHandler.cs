using Application.Models;
using AutoMapper;
using Domain.Entities;
using Domain.Repositories;
using MediatR;

namespace Application.Commands.Surgeries
{
    /// <summary>
    /// Handler for creating a new Surgery.
    /// </summary>
    public class CreateSurgeryCommandHandler : IRequestHandler<CreateSurgeryCommand, int>
    {
        private readonly ISurgeryRepository _surgeryRepository;
        private readonly IMapper _mapper;

        public CreateSurgeryCommandHandler(ISurgeryRepository surgeryRepository, IMapper mapper)
        {
            _surgeryRepository = surgeryRepository;
            _mapper = mapper;
        }

        public async Task<int> Handle(CreateSurgeryCommand request, CancellationToken cancellationToken)
        {
            // Map SurgeryDto to Surgery entity
            var surgery = _mapper.Map<Surgery>(request.SurgeryDto);

            surgery.CreatedAt = DateTime.Now;
            surgery.UpdatedAt = DateTime.Now;

            await _surgeryRepository.AddAsync(surgery);
            return surgery.Id;
        }
    }
}