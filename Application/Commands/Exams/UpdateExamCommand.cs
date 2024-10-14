using Application.Models;
using MediatR;

namespace Application.Commands.Exams
{
    /// <summary>
    /// Command to update an existing exam.
    /// </summary>
    public class UpdateExamCommand : IRequest<Unit>
    {
        public int Id { get; set; }
        public ExamDto ExamDto { get; set; }

        public UpdateExamCommand(int id, ExamDto examDto)
        {
            Id = id;
            ExamDto = examDto;
        }
    }
}