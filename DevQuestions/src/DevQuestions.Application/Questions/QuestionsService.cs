using CSharpFunctionalExtensions;
using DevQuestions.Application.Communication;
using DevQuestions.Application.DataBase;
using DevQuestions.Application.Extensions;
using DevQuestions.Application.FulltextSearch;
using DevQuestions.Application.Questions.Fails;
using DevQuestions.Application.Questions.Fails.Exceptions;
using DevQuestions.Contracts.Questions;
using DevQuestions.Domain.Questions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using Shared;

namespace DevQuestions.Application.Questions;

public class QuestionsService : IQuestionsService
{
    private readonly ILogger<QuestionsService> _logger;
    private readonly IQuestionsRepository _questionsRepository;
    private readonly IValidator<CreateQuestionDto> _createQuestionDtoValidator;
    private readonly IValidator<AddAnswerDto> _addAnswerDtoValidator;
    private readonly ITransactionManager _transactionManager;
    private readonly IUsersCommunicationService _usersCommunicationService;

    public QuestionsService(
        IQuestionsRepository questionsRepository,
        ILogger<QuestionsService> logger,
        IValidator<CreateQuestionDto> createQuestionDtoValidator,
        IValidator<AddAnswerDto> addAnswerDtoValidator,
        ITransactionManager transactionManager,
        IUsersCommunicationService usersCommunicationService)
    {
        _logger = logger;
        _createQuestionDtoValidator = createQuestionDtoValidator;
        _addAnswerDtoValidator = addAnswerDtoValidator;
        _transactionManager = transactionManager;
        _usersCommunicationService = usersCommunicationService;
        _questionsRepository = questionsRepository;
    }

    // public async Task<IActionResult> Update(
    //     [FromRoute] Guid questionId,
    //     [FromBody] UpdateQuestionDto request,
    //     CancellationToken cancellationToken)
    // { 
    // } 
    // public async Task<IActionResult> Delete(Guid questionId, CancellationToken cancellationToken)
    // {
    //     var question = await _questionsRepository.GetByIdAsync(questionId, cancellationToken);
    //     
    // }
    //
    // public async Task<IActionResult> SelectSolution(
    //     Guid questionId,
    //     Guid answerId,
    //     CancellationToken cancellationToken)
    // {
    // }
    //
    public Task<Result<Guid, Failure>> Create(CreateQuestionDto questionDto, CancellationToken cancellationToken) => throw new NotImplementedException();

    public async Task<Result<Guid, Failure>> AddAnswer(
        Guid questionId,
        AddAnswerDto addAnswerDto,
        CancellationToken cancellationToken)
    {
        var validationResult = await _addAnswerDtoValidator.ValidateAsync(addAnswerDto, cancellationToken);
        if (validationResult.IsValid == false)
        {
            return validationResult.ToErrors();
        }

        var usersRatingResult = await _usersCommunicationService.GetUserRating(addAnswerDto.UserId, cancellationToken);
        if (usersRatingResult.IsFailure)
        {
            return usersRatingResult.Error;
        }

        if (usersRatingResult.Value <= 0)
        {
            _logger.LogError("User with id {userId} has no rating", addAnswerDto.UserId);
            return Errors.Questions.NotEnoughRating();
        }

        var transaction = await _transactionManager.BeginTransactionAsync(cancellationToken);

        (_, bool isFailure, Question? question, Failure? error) = await _questionsRepository.GetByIdAsync(questionId, cancellationToken);
        if (isFailure)
        {
            return error;
        }

        var answer = new Answer(Guid.NewGuid(), addAnswerDto.UserId, addAnswerDto.Text, questionId);

        question.Answers.Add(answer);

        await _questionsRepository.SaveAsync(question, cancellationToken);

        transaction.Commit();

        _logger.LogInformation("Answer added with id {anserId} to question {questionId}", answer.Id, questionId);

        return answer.Id;
    }
}

public class QuestionCalculate()
{
    public Result<int, Failure> Calculate()
    {
        // операция
        return Error.Failure("", "").ToFailure();
    }
}