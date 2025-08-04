using DevQuestions.Application.Extensions;
using DevQuestions.Application.FulltextSearch;
using DevQuestions.Application.Questions.Exceptions;
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
    private readonly ISearchProvider _searchProvider;
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
            throw new QuestionValidationException(validationResult.ToErrors());
        }

        var calculator = new QuestionCalculate();

        Result calculateResult = calculator.Calculate();
        if (calculateResult.IsFailure)
        {
        }

        int openUserQuestionCount = await _questionsRepository
            .GetOpenUserQuestionsAsync(questionDto.UserId, cancellationToken);

        var existedQuestion = await _questionsRepository.GetByIdAsync(Guid.Empty, cancellationToken);

        if (openUserQuestionCount > 3)
        {
            throw new ToManyQuestionsException();
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
    // public async Task<IActionResult> AddAnswer(
    //     Guid questionId,
    //     AddAnswerDto request,
    //     CancellationToken cancellationToken)
    // {
    // }
}

public class QuestionCalculate()
{
    public Result Calculate()
    {
        // операция
        return Result.Success();
    }
}

public class Result
{
    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;
    public Error Error { get; }

    protected Result()
    {
        IsSuccess = true;
        Error = Error.None;
    }

    protected Result(Error error)
    {
        IsSuccess = false;
        Error = error;
    }

    public static Result Failure(Error error) => new (error);

    public static Result Success() => new();
}

public sealed class Result<TValue> : Result
{
    private readonly TValue _value = default!;

    private Result(TValue value) => _value = value;

    private Result(Error error)
        : base(error)
    {
    }

    public new static Result<TValue> Failure(Error error) => new(error);

    public static Result<TValue> Success(TValue value) => new (value);

    // public static implicit operator Result<TValue>(Error error) => Failure(error);
    //
    // public static implicit operator Result<TValue>(TValue value) => Success(value);
    //
    // public static implicit operator TValue(Result<TValue> value) => value._value;
    
    public TValue Value => IsSuccess ? _value : throw new ResultException();
}