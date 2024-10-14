using Application.Models;
using AutoMapper;
using Domain.Entities;
using Domain.Repositories;
using MediatR;

namespace Application.Commands.Exams
{
    /// <summary>
    /// Handler for creating an exam.
    /// Uses AutoMapper to map from ExamDto to the Exam entity.
    /// </summary>
    public class CreateExamCommandHandler : IRequestHandler<CreateExamCommand, int>
    {
        private readonly IExamRepository _examRepository;
        private readonly IMapper _mapper;

        public CreateExamCommandHandler(IExamRepository examRepository, IMapper mapper)
        {
            _examRepository = examRepository;
            _mapper = mapper;
        }

        public async Task<int> Handle(CreateExamCommand request, CancellationToken cancellationToken)
        {
            // Map ExamDto to Exam entity
            var exam = _mapper.Map<Exam>(request.ExamDto);

            // Set creation and update timestamps
            exam.CreatedAt = DateTime.Now;
            exam.UpdatedAt = DateTime.Now;

            await _examRepository.AddAsync(exam);
            return exam.Id;
        }
    }
}