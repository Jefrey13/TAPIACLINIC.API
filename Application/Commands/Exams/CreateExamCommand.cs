using Application.Models;
using MediatR;

namespace Application.Commands.Exams
{
    /// <summary>
    /// Command to create a new exam.
    /// This command contains the data required to create an exam via the ExamDto.
    /// </summary>
    public class CreateExamCommand : IRequest<int>
    {
        public ExamDto ExamDto { get; set; }

        public CreateExamCommand(ExamDto examDto)
        {
            ExamDto = examDto;
        }
    }
}