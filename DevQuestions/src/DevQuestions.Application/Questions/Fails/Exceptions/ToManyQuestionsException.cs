using DevQuestions.Application.Exceptions;
using Shared;

namespace DevQuestions.Application.Questions.Fails.Exceptions;

public class ToManyQuestionsException : BadRequestException // 500
{
    public ToManyQuestionsException()
        : base([Errors.Questions.ToManyQuestions()])
    {
    }
}