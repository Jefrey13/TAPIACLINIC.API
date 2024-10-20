using Application.Exceptions;
using AutoMapper;
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
    /// Handler for updating a state.
    /// </summary>
    public class UpdateStateCommandHandler : IRequestHandler<UpdateStateCommand, Unit>
    {
        private readonly IStateRepository _stateRepository;
        private readonly IMapper _mapper;

        public UpdateStateCommandHandler(IStateRepository stateRepository, IMapper mapper)
        {
            _stateRepository = stateRepository;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateStateCommand request, CancellationToken cancellationToken)
        {
            var state = await _stateRepository.GetByIdAsync(request.Id);
            if (state == null)
            {
                throw new NotFoundException(nameof(State), request.Id); // Custom exception for entity not found
            }

            _mapper.Map(request.StateDto, state);
            state.UpdatedAt = DateTime.Now;

            await _stateRepository.UpdateAsync(state);
            return Unit.Value;
        }
    }
}
