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
    /// Handler for retrieving all states.
    /// </summary>
    public class GetAllStatesQueryHandler : IRequestHandler<GetAllStatesQuery, IEnumerable<StateDto>>
    {
        private readonly IStateRepository _stateRepository;
        private readonly IMapper _mapper;

        public GetAllStatesQueryHandler(IStateRepository stateRepository, IMapper mapper)
        {
            _stateRepository = stateRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<StateDto>> Handle(GetAllStatesQuery request, CancellationToken cancellationToken)
        {
            var states = await _stateRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<StateDto>>(states);
        }
    }
}
