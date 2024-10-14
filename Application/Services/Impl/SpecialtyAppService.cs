using Application.Commands.Specialties;
using Application.Models;
using Application.Queries.Specialties;
using AutoMapper;
using MediatR;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Services.Impl
{
    /// <summary>
    /// Implements the operations related to managing medical specialties.
    /// Utilizes MediatR for command and query handling and AutoMapper for DTO mapping.
    /// </summary>
    public class SpecialtyAppService : ISpecialtyAppService
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public SpecialtyAppService(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        /// <summary>
        /// Sends a command to create a new medical specialty.
        /// </summary>
        /// <param name="command">Contains specialty data in a DTO format.</param>
        /// <returns>The ID of the newly created specialty.</returns>
        public async Task<int> CreateSpecialtyAsync(CreateSpecialtyCommand command)
        {
            return await _mediator.Send(command);
        }

        /// <summary>
        /// Sends a command to update an existing specialty.
        /// </summary>
        /// <param name="command">The updated specialty details.</param>
        public async Task UpdateSpecialtyAsync(UpdateSpecialtyCommand command)
        {
            await _mediator.Send(command);
        }

        /// <summary>
        /// Sends a command to delete a specialty by its ID.
        /// </summary>
        /// <param name="id">The unique ID of the specialty to delete.</param>
        public async Task DeleteSpecialtyAsync(int id)
        {
            await _mediator.Send(new DeleteSpecialtyCommand(id));
        }

        /// <summary>
        /// Queries all specialties and maps them to DTOs.
        /// </summary>
        /// <returns>A list of all specialties as DTOs.</returns>
        public async Task<IEnumerable<SpecialtyDto>> GetAllSpecialtiesAsync()
        {
            return await _mediator.Send(new GetAllSpecialtiesQuery());
        }

        /// <summary>
        /// Retrieves a specialty by its ID.
        /// </summary>
        /// <param name="id">The unique ID of the specialty.</param>
        /// <returns>The DTO of the retrieved specialty.</returns>
        public async Task<SpecialtyDto> GetSpecialtyByIdAsync(int id)
        {
            return await _mediator.Send(new GetSpecialtyByIdQuery(id));
        }
    }
}