using Application.Commands.Surgeries;
using Application.Models;
using Application.Queries.Surgeries;
using AutoMapper;
using MediatR;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Services.Impl
{
    /// <summary>
    /// Implements operations related to managing surgeries.
    /// This class uses MediatR for command and query handling, and AutoMapper for entity-to-DTO mapping.
    /// </summary>
    public class SurgeryAppService : ISurgeryAppService
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public SurgeryAppService(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        /// <summary>
        /// Creates a new surgery by sending a command through MediatR.
        /// </summary>
        /// <param name="command">The surgery data in DTO format.</param>
        /// <returns>The ID of the newly created surgery.</returns>
        public async Task<int> CreateSurgeryAsync(CreateSurgeryCommand command)
        {
            return await _mediator.Send(command);
        }

        /// <summary>
        /// Updates an existing surgery by sending a command through MediatR.
        /// </summary>
        /// <param name="command">The updated surgery details.</param>
        public async Task UpdateSurgeryAsync(UpdateSurgeryCommand command)
        {
            await _mediator.Send(command);
        }

        /// <summary>
        /// Deletes a surgery by its ID using MediatR to send the delete command.
        /// </summary>
        /// <param name="id">The unique ID of the surgery to be deleted.</param>
        public async Task DeleteSurgeryAsync(int id)
        {
            await _mediator.Send(new DeleteSurgeryCommand(id));
        }

        /// <summary>
        /// Retrieves all surgeries by sending a query through MediatR and maps them to DTOs.
        /// </summary>
        /// <returns>A list of all surgeries as DTOs.</returns>
        public async Task<IEnumerable<SurgeryDto>> GetAllSurgeriesAsync()
        {
            return await _mediator.Send(new GetAllSurgeriesQuery());
        }

        /// <summary>
        /// Retrieves a surgery by its ID using MediatR to send a query.
        /// </summary>
        /// <param name="id">The unique ID of the surgery.</param>
        /// <returns>The DTO of the retrieved surgery.</returns>
        public async Task<SurgeryDto> GetSurgeryByIdAsync(int id)
        {
            return await _mediator.Send(new GetSurgeryByIdQuery(id));
        }
    }
}