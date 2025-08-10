using CSharpFunctionalExtensions;
using DevQuestions.Contracts.Questions;
using Shared;

namespace DevQuestions.Application.Questions;

public interface IQuestionsService
{
    /// <summary>
    /// Создание вопроса.
    /// </summary>
    /// <param name="questionDto">qustion Dto.</param>
    /// <param name="cancellationToken">Токен для отмены.</param>
    /// <returns>ID созданного вопроса</returns>
    Task<Result<Guid, Failure>> Create(CreateQuestionDto questionDto, CancellationToken cancellationToken);

    /// <summary>
    /// Добавление ответа на вопрос
    /// </summary>
    /// <param name="questionId">ID вопроса.</param>
    /// <param name="addAnswerDto">DTO для создания ответа.</param>
    /// <param name="cancellationToken">Токен для отмены.</param>
    /// <returns>ID добавленного ответа.</returns>
    Task<Result<Guid, Failure>> AddAnswer(Guid questionId, AddAnswerDto addAnswerDto, CancellationToken cancellationToken);
}