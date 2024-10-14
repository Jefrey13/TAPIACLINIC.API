using Application.Exceptions;
using AutoMapper;
using Domain.Entities;
using Domain.Repositories;
using MediatR;

namespace Application.Commands.Surgeries
{
    /// <summary>
    /// Handler for updating a Surgery.
    /// </summary>
    public class UpdateSurgeryCommandHandler : IRequestHandler<UpdateSurgeryCommand, Unit>
    {
        private readonly ISurgeryRepository _surgeryRepository;
        private readonly IMapper _mapper;

        public UpdateSurgeryCommandHandler(ISurgeryRepository surgeryRepository, IMapper mapper)
        {
            _surgeryRepository = surgeryRepository;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateSurgeryCommand request, CancellationToken cancellationToken)
        {
            var surgery = await _surgeryRepository.GetByIdAsync(request.Id);
            if (surgery == null)
            {
                throw new NotFoundException(nameof(Surgery), request.Id);
            }

            // Use AutoMapper to map SurgeryDto to the existing Surgery entity
            _mapper.Map(request.SurgeryDto, surgery);

            surgery.UpdatedAt = DateTime.Now;

            await _surgeryRepository.UpdateAsync(surgery);
            return Unit.Value;
        }
    }
}