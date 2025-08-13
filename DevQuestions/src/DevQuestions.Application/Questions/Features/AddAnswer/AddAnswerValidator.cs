using DevQuestions.Contracts.Questions;
using FluentValidation;

namespace DevQuestions.Application.Questions.Features.AddAnswer;

public class AddAnswerValidator : AbstractValidator<AddAnswerDto>
{
    public AddAnswerValidator()
    {
        RuleFor(x => x.Text)
            .NotEmpty().WithMessage("Текст не может быть пустым").WithErrorCode("title.code")
            .MaximumLength(5000).WithMessage("Текст невалидныsй");
    }
}