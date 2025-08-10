using CSharpFunctionalExtensions;
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

    public QuestionsService(
        IQuestionsRepository questionsRepository,
        ILogger<QuestionsService> logger,
        IValidator<CreateQuestionDto> createQuestionDtoValidator,
        IValidator<AddAnswerDto> addAnswerDtoValidator)
    {
        _logger = logger;
        _createQuestionDtoValidator = createQuestionDtoValidator;
        _addAnswerDtoValidator = addAnswerDtoValidator;
        _questionsRepository = questionsRepository;
    }

    public async Task<Result<Guid, Failure>> Create(CreateQuestionDto questionDto, CancellationToken cancellationToken)
    {
        var validationResult = await _createQuestionDtoValidator.ValidateAsync(questionDto, cancellationToken);
        if (validationResult.IsValid == false)
        {
            return validationResult.ToErrors();
        }

        var calculator = new QuestionCalculate();

        var calculateResult = calculator.Calculate();
        if (calculateResult.IsFailure)
        {
            return calculateResult.Error;
        }

        int value = calculateResult.Value;

        int openUserQuestionCount = await _questionsRepository
            .GetOpenUserQuestionsAsync(questionDto.UserId, cancellationToken);

        var existedQuestion = await _questionsRepository.GetByIdAsync(Guid.Empty, cancellationToken);

        if (openUserQuestionCount > 3)
        {
            return Errors.Questions.ToManyQuestions().ToFailure();
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

        var questionResult = await _questionsRepository.GetByIdAsync(questionId, cancellationToken);
        if (questionResult.IsFailure)
        {
            return questionResult.Error;
        }

        var answer = new Answer(Guid.NewGuid(), addAnswerDto.UserId, addAnswerDto.Text, questionId);

        var answerId = await _questionsRepository.AddAnswerAsync(answer, cancellationToken);

        _logger.LogInformation("Answer added with id {anserId} to question {questionId}", answerId, questionId);

        return answerId;
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