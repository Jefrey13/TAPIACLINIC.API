using Application.Commands.Exams;
using Application.Models;
using Application.Queries.Exams;
using AutoMapper;
using MediatR;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Services.Impl
{
    /// <summary>
    /// Implements the operations related to managing exams.
    /// This class uses MediatR for handling commands and queries, and AutoMapper for entity-to-DTO mapping.
    /// </summary>
    public class ExamAppService : IExamAppService
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public ExamAppService(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        /// <summary>
        /// Creates a new exam using MediatR to send the creation command.
        /// </summary>
        /// <param name="command">Contains the exam details in a DTO format.</param>
        /// <returns>The ID of the newly created exam.</returns>
        public async Task<int> CreateExamAsync(CreateExamCommand command)
        {
            return await _mediator.Send(command);
        }

        /// <summary>
        /// Updates an existing exam using MediatR to send the update command.
        /// </summary>
        /// <param name="command">Contains the updated exam details.</param>
        public async Task UpdateExamAsync(UpdateExamCommand command)
        {
            await _mediator.Send(command);
        }

        /// <summary>
        /// Deletes an exam by its ID using MediatR to send the delete command.
        /// </summary>
        /// <param name="id">The unique ID of the exam to be deleted.</param>
        public async Task DeleteExamAsync(int id)
        {
            await _mediator.Send(new DeleteExamCommand(id));
        }

        /// <summary>
        /// Retrieves all exams by sending a query through MediatR and maps them to DTOs.
        /// </summary>
        /// <returns>A list of all exams as DTOs.</returns>
        public async Task<IEnumerable<ExamDto>> GetAllExamsAsync()
        {
            return await _mediator.Send(new GetAllExamsQuery());
        }

        /// <summary>
        /// Retrieves an exam by its unique ID using MediatR to send a query.
        /// </summary>
        /// <param name="id">The unique ID of the exam.</param>
        /// <returns>The DTO of the retrieved exam.</returns>
        public async Task<ExamDto> GetExamByIdAsync(int id)
        {
            return await _mediator.Send(new GetExamByIdQuery(id));
        }
    }
}