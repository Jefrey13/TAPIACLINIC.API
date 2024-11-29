using Application.Exceptions;
using Domain.Entities;
using Domain.Repositories;
using MediatR;

namespace Application.Commands.Exams
{
    /// <summary>
    /// Handler for deleting an exam.
    /// Throws a NotFoundException if the exam doesn't exist.
    /// </summary>
    public class DeleteExamCommandHandler : IRequestHandler<DeleteExamCommand, Unit>
    {
        private readonly IExamRepository _examRepository;

        public DeleteExamCommandHandler(IExamRepository examRepository)
        {
            _examRepository = examRepository;
        }

        public async Task<Unit> Handle(DeleteExamCommand request, CancellationToken cancellationToken)
        {
            var exam = await _examRepository.GetByIdAsync(request.Id);
            if (exam == null)
            {
                throw new NotFoundException(nameof(Exam), request.Id);
            }

            await _examRepository.ToggleActiveStateAsync(exam);
            return Unit.Value;
        }
    }
}