using Application.Commands.Menus;
using Application.Models;
using Application.Queries.Menus;
using AutoMapper;
using MediatR;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Services.Impl
{
    /// <summary>
    /// Implements the operations related to managing menus.
    /// This class uses MediatR for handling commands and queries, and AutoMapper for entity-to-DTO mapping.
    /// </summary>
    public class MenuAppService : IMenuAppService
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public MenuAppService(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        public async Task<int> CreateMenuAsync(CreateMenuCommand command)
        {
            return await _mediator.Send(command);
        }

        public async Task UpdateMenuAsync(UpdateMenuCommand command)
        {
            await _mediator.Send(command);
        }

        public async Task DeleteMenuAsync(DeleteMenuCommand command)
        {
            await _mediator.Send(command);
        }

        public async Task<IEnumerable<MenuDto>> GetAllMenusAsync()
        {
            return await _mediator.Send(new GetAllMenusQuery());
        }

        public async Task<MenuDto> GetMenuByIdAsync(int id)
        {
            return await _mediator.Send(new GetMenuByIdQuery(id));
        }
    }
}