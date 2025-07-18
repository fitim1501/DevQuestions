using DevQuestions.Contracts.Questions;
using DevQuestions.Domain.Questions;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace DevQuestions.Application.Questions;

public class QuestionsService : IQuestionsService
{
    private readonly ILogger<QuestionsService> _logger;
    private readonly IQuestionsRepository _questionsRepository;
    private readonly IValidator<CreateQuestionDto> _validator;

    public QuestionsService(
        IQuestionsRepository questionsRepository,
        ILogger<QuestionsService> logger,
        IValidator<CreateQuestionDto> validator)
    {
        _logger = logger;
        _validator = validator;
        _questionsRepository = questionsRepository;
    }

    public async Task<Guid> Create(CreateQuestionDto questionDto, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(questionDto, cancellationToken);
        if (validationResult.IsValid == false)
        {
            throw new ValidationException(validationResult.Errors);
        }

        int getOpenUserQuestionsCount = await _questionsRepository.GetOpenUserQuestionsAsync(questionDto.UserId, cancellationToken);

        if (getOpenUserQuestionsCount > 3)
        {
            throw new Exception("Пользователь не может больше 3 вопросов.");
        }

        var questionId = Guid.NewGuid();

        var question = new Question(
            questionId,
            questionDto.Title,
            questionDto.Text,
            questionDto.UserId,
            null,
            questionDto.TagIds);

        await _questionsRepository.AddAsync(question, cancellationToken);

        _logger.LogInformation("Question created with id {questionId}", questionId);

        return questionId;
    }

    // public async Task<IActionResult> Update(
    //     [FromRoute] Guid questionId,
    //     [FromBody] UpdateQuestionDto request,
    //     CancellationToken cancellationToken)
    // { 
    // } 
    // public async Task<IActionResult> Delete(Guid questionId, CancellationToken cancellationToken)
    // {
    // }
    //
    // public async Task<IActionResult> SelectSolution(
    //     Guid questionId,
    //     Guid answerId,
    //     CancellationToken cancellationToken)
    // {
    // }
    //
    // public async Task<IActionResult> AddAnswer(
    //     Guid questionId,
    //     AddAnswerDto request,
    //     CancellationToken cancellationToken)
    // {
    // }
}