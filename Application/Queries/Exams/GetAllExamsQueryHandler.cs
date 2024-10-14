using Application.Models;
using AutoMapper;
using Domain.Repositories;
using MediatR;

namespace Application.Queries.Exams
{
    /// <summary>
    /// Handler for retrieving all exams.
    /// Maps from Exam entity to ExamDto.
    /// </summary>
    public class GetAllExamsQueryHandler : IRequestHandler<GetAllExamsQuery, IEnumerable<ExamDto>>
    {
        private readonly IExamRepository _examRepository;
        private readonly IMapper _mapper;

        public GetAllExamsQueryHandler(IExamRepository examRepository, IMapper mapper)
        {
            _examRepository = examRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ExamDto>> Handle(GetAllExamsQuery request, CancellationToken cancellationToken)
        {
            var exams = await _examRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<ExamDto>>(exams);  // Map Exam entities to DTOs
        }
    }
}