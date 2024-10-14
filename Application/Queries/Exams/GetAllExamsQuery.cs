using Application.Models;
using MediatR;

namespace Application.Queries.Exams
{
    /// <summary>
    /// Query to get all exams.
    /// </summary>
    public class GetAllExamsQuery : IRequest<IEnumerable<ExamDto>>
    {
    }
}