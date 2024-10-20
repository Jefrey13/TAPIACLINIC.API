using Application.Commands.States;
using Application.Models;
using Application.Queries.States;
using MediatR;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Services.Impl
{
    /// <summary>
    /// Implements the operations related to managing states.
    /// This class uses MediatR to send commands and queries.
    /// </summary>
    public class StateAppService : IStateAppService
    {
        private readonly IMediator _mediator;

        public StateAppService(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Creates a new state by sending the CreateStateCommand to MediatR.
        /// </summary>
        public async Task<int> CreateStateAsync(CreateStateCommand command)
        {
            return await _mediator.Send(command);
        }

        /// <summary>
        /// Updates an existing state by sending the UpdateStateCommand to MediatR.
        /// </summary>
        public async Task UpdateStateAsync(UpdateStateCommand command)
        {
            await _mediator.Send(command);
        }

        /// <summary>
        /// Deletes a state by sending the DeleteStateCommand to MediatR.
        /// </summary>
        public async Task DeleteStateAsync(DeleteStateCommand command)
        {
            await _mediator.Send(command);
        }

        /// <summary>
        /// Retrieves all states by sending the GetAllStatesQuery to MediatR.
        /// </summary>
        public async Task<IEnumerable<StateDto>> GetAllStatesAsync()
        {
            return await _mediator.Send(new GetAllStatesQuery());
        }

        /// <summary>
        /// Retrieves a state by its ID by sending the GetStateByIdQuery to MediatR.
        /// </summary>
        public async Task<StateDto> GetStateByIdAsync(int id)
        {
            return await _mediator.Send(new GetStateByIdQuery(id));
        }
    }
}