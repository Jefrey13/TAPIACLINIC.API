using Application.Commands.Exams;
using FluentValidation;

namespace Application.Validators
{
    /// <summary>
    /// Validator for UpdateExamCommand.
    /// Validates the properties of the ExamDto within the command.
    /// </summary>
    public class UpdateExamCommandValidator : AbstractValidator<UpdateExamCommand>
    {
        public UpdateExamCommandValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("Exam ID must be greater than 0.");

            RuleFor(x => x.ExamDto.Name)
                .NotEmpty().WithMessage("Exam name is required.")
                .MaximumLength(100).WithMessage("Exam name must not exceed 100 characters.");

            RuleFor(x => x.ExamDto.Description)
                .MaximumLength(255).WithMessage("Exam description must not exceed 255 characters.");
        }
    }
}