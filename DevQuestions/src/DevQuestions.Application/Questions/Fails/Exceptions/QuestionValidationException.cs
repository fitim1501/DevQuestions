using DevQuestions.Application.Exceptions;
using Shared;

namespace DevQuestions.Application.Questions.Fails.Exceptions;

public class QuestionValidationException : BadRequestException // 500
{
    public QuestionValidationException(Error[] errors)
        : base(errors)
    {
    }
}