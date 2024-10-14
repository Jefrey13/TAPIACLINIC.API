using Application.Exceptions;
using Application.Models;
using AutoMapper;
using Domain.Entities;
using Domain.Repositories;
using MediatR;

namespace Application.Commands.Exams
{
    /// <summary>
    /// Handler for updating an exam.
    /// Uses AutoMapper to map from ExamDto to the Exam entity.
    /// </summary>
    public class UpdateExamCommandHandler : IRequestHandler<UpdateExamCommand, Unit>
    {
        private readonly IExamRepository _examRepository;
        private readonly IMapper _mapper;

        public UpdateExamCommandHandler(IExamRepository examRepository, IMapper mapper)
        {
            _examRepository = examRepository;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateExamCommand request, CancellationToken cancellationToken)
        {
            var exam = await _examRepository.GetByIdAsync(request.Id);
            if (exam == null)
            {
                throw new NotFoundException(nameof(Exam), request.Id);
            }

            // Map ExamDto to existing Exam entity
            _mapper.Map(request.ExamDto, exam);

            // Update timestamp
            exam.UpdatedAt = DateTime.Now;

            await _examRepository.UpdateAsync(exam);
            return Unit.Value;
        }
    }
}