using DevQuestions.Contracts;
using DevQuestions.Domain.Questions;

namespace DevQuestions.Application;

public class QuestionsService
{
    public QuestionsService()
    {
        
    }
    /*
    public async Task Create(CreateQuestionDto questionDto, CancellationToken cancellationToken)
    {
        // проверка валидности

        // создание сущности Question
        var questionId = Guid.NewGuid();

        var question = new Question(
            Id = questionId,
            Title = questionDto.Title,
            Text = questionDto.Text,
            UserId = questionDto.UserId,
            screenshotId = null,
            Tags = questionDto.TagIds);

        // Сохранение сущности в БД
        // Логирование об успешном или неуспешном сохранении 
    }
    /*
    public async Task<IActionResult> Update(
        [FromRoute] Guid questionId,
        [FromBody] UpdateQuestionDto request,
        CancellationToken cancellationToken)
    { 
    } 
    public async Task<IActionResult> Delete(Guid questionId, CancellationToken cancellationToken)
    {
    }

    public async Task<IActionResult> SelectSolution(
        Guid questionId,
        Guid answerId,
        CancellationToken cancellationToken)
    {
    }

    public async Task<IActionResult> AddAnswer(
        Guid questionId,
        AddAnswerDto request,
        CancellationToken cancellationToken)
    {
    }*/
}