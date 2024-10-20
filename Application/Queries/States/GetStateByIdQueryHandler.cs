using Application.Models;
using AutoMapper;
using Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.States
{
    /// <summary>
    /// Handler for retrieving a state by its ID.
    /// </summary>
    public class GetStateByIdQueryHandler : IRequestHandler<GetStateByIdQuery, StateDto>
    {
        private readonly IStateRepository _stateRepository;
        private readonly IMapper _mapper;

        public GetStateByIdQueryHandler(IStateRepository stateRepository, IMapper mapper)
        {
            _stateRepository = stateRepository;
            _mapper = mapper;
        }

        public async Task<StateDto> Handle(GetStateByIdQuery request, CancellationToken cancellationToken)
        {
            var state = await _stateRepository.GetByIdAsync(request.Id);
            return _mapper.Map<StateDto>(state);
        }
    }
}
