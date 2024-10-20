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
    /// Handler for creating a new state.
    /// </summary>
    public class CreateStateCommandHandler : IRequestHandler<CreateStateCommand, int>
    {
        private readonly IStateRepository _stateRepository;
        private readonly IMapper _mapper;

        public CreateStateCommandHandler(IStateRepository stateRepository, IMapper mapper)
        {
            _stateRepository = stateRepository;
            _mapper = mapper;
        }

        public async Task<int> Handle(CreateStateCommand request, CancellationToken cancellationToken)
        {
            var state = _mapper.Map<State>(request.StateDto);
            state.CreatedAt = DateTime.Now;
            state.UpdatedAt = DateTime.Now;

            await _stateRepository.AddAsync(state);
            return state.Id;
        }
    }
}
