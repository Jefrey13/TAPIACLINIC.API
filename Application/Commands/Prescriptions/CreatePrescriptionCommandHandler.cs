using Application.Commands.Prescriptions;
using AutoMapper;
using Domain.Entities;
using Domain.Repositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Handlers.Prescriptions
{
    /// <summary>
    /// Handler for CreatePrescriptionCommand.
    /// Handles the creation of a new prescription.
    /// </summary>
    public class CreatePrescriptionCommandHandler : IRequestHandler<CreatePrescriptionCommand, int>
    {
        private readonly IPrescriptionRepository _repository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the CreatePrescriptionCommandHandler class.
        /// </summary>
        /// <param name="repository">The repository to handle the creation.</param>
        /// <param name="mapper">Mapper to convert DTO to entity.</param>
        public CreatePrescriptionCommandHandler(IPrescriptionRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        /// <summary>
        /// Handles the creation of a prescription.
        /// </summary>
        /// <param name="request">The command containing the prescription details.</param>
        /// <param name="cancellationToken">Cancellation token for the request.</param>
        /// <returns>The ID of the newly created prescription.</returns>
        public async Task<int> Handle(CreatePrescriptionCommand request, CancellationToken cancellationToken)
        {
            var prescription = _mapper.Map<Prescription>(request.PrescriptionDto);
            await _repository.AddAsync(prescription);
            return prescription.Id;
        }
    }
}