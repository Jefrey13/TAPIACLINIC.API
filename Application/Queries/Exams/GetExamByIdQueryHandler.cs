using Application.Exceptions;
using Application.Models;
using AutoMapper;
using Domain.Entities;
using Domain.Repositories;
using MediatR;

namespace Application.Queries.Exams
{
    /// <summary>
    /// Handler for retrieving an exam by its ID.
    /// Maps from Exam entity to ExamDto.
    /// </summary>
    public class GetExamByIdQueryHandler : IRequestHandler<GetExamByIdQuery, ExamDto>
    {
        private readonly IExamRepository _examRepository;
        private readonly IMapper _mapper;

        public GetExamByIdQueryHandler(IExamRepository examRepository, IMapper mapper)
        {
            _examRepository = examRepository;
            _mapper = mapper;
        }

        public async Task<ExamDto> Handle(GetExamByIdQuery request, CancellationToken cancellationToken)
        {
            var exam = await _examRepository.GetByIdAsync(request.Id);
            if (exam == null)
            {
                throw new NotFoundException(nameof(Exam), request.Id);
            }

            return _mapper.Map<ExamDto>(exam);  // Map entity to DTO
        }
    }
}
