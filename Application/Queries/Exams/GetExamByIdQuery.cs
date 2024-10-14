using Application.Models;
using MediatR;

namespace Application.Queries.Exams
{
    /// <summary>
    /// Query to get an exam by its ID.
    /// </summary>
    public class GetExamByIdQuery : IRequest<ExamDto>
    {
        public int Id { get; set; }

        public GetExamByIdQuery(int id)
        {
            Id = id;
        }
    }
}