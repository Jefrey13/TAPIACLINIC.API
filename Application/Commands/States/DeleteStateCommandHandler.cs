using Application.Exceptions;
using Domain.Entities;
using Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.States
{
    /// <summary>
    /// Handler for deleting a state.
    /// </summary>
    public class DeleteStateCommandHandler : IRequestHandler<DeleteStateCommand, Unit>
    {
        private readonly IStateRepository _stateRepository;

        public DeleteStateCommandHandler(IStateRepository stateRepository)
        {
            _stateRepository = stateRepository;
        }

        public async Task<Unit> Handle(DeleteStateCommand request, CancellationToken cancellationToken)
        {
            var state = await _stateRepository.GetByIdAsync(request.Id);
            if (state == null)
            {
                throw new NotFoundException(nameof(State), request.Id); // Custom exception for entity not found
            }

            await _stateRepository.ToggleActiveStateAsync(state);
            return Unit.Value;
        }
    }
}
