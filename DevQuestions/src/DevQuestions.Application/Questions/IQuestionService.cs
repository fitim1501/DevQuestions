using DevQuestions.Contracts.Questions;

namespace DevQuestions.Application.Questions;

public interface IQuestionService
{
    Task Create(CreateQuestionDto questionDto, CancellationToken cancellationToken);
}