using CSharpFunctionalExtensions;
using DevQuestions.Application.Abstractions;
using DevQuestions.Application.Communication;
using DevQuestions.Application.DataBase;
using DevQuestions.Application.Extensions;
using DevQuestions.Application.Questions.Fails;
using DevQuestions.Contracts.Questions;
using DevQuestions.Contracts.Questions.Dtos;
using DevQuestions.Domain.Questions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using Shared;

namespace DevQuestions.Application.Questions.Features.AddAnswer;
public class AddAnswerHandler : IHandler<Guid, AddAnswerCommand>
{
    private readonly ILogger<AddAnswerHandler> _logger;
    private readonly IValidator<AddAnswerDto> _addAnswerDtoValidator;
    private readonly IUsersCommunicationService _usersCommunicationService;
    private readonly ITransactionManager _transactionManager;
    private readonly IQuestionsRepository _questionsRepository;
    public AddAnswerHandler(
        IValidator<AddAnswerDto> addAnswerDtoValidator,
        ILogger<AddAnswerHandler> logger,
        IUsersCommunicationService usersCommunicationService,
        ITransactionManager transactionManager,
        IQuestionsRepository questionsRepository)
    {
        _addAnswerDtoValidator = addAnswerDtoValidator;
        _logger = logger;
        _usersCommunicationService = usersCommunicationService;
        _transactionManager = transactionManager;
        _questionsRepository = questionsRepository;
    }
    public async Task<Result<Guid, Failure>> Handle(
        AddAnswerCommand command,
        CancellationToken cancellationToken)
    {
        var validationResult = await _addAnswerDtoValidator.ValidateAsync(command.AddAnswerDto,cancellationToken);
        if (validationResult.IsValid == false)
        {
            return validationResult.ToErrors();
        }

        var usersRatingResult = await _usersCommunicationService.GetUserRating(command.AddAnswerDto.UserId, cancellationToken);
        if (usersRatingResult.IsFailure)
        {
            return usersRatingResult.Error;
        }

        if (usersRatingResult.Value <= 0)
        {
            _logger.LogError("User with id {userId} has no rating", command.AddAnswerDto.UserId);
            return Errors.Questions.NotEnoughRating();
        }

        var transaction = await _transactionManager.BeginTransactionAsync(cancellationToken);

        (_, bool isFailure, Question? question, Failure? error) = await _questionsRepository.GetByIdAsync(command.QuestionId, cancellationToken);
        if (isFailure)
        {
            return error;
        }

        var answer = new Answer(Guid.NewGuid(), command.AddAnswerDto.UserId, command.AddAnswerDto.Text, command.QuestionId);

        question.Answers.Add(answer);

        await _questionsRepository.SaveAsync(question, cancellationToken);

        transaction.Commit();

        _logger.LogInformation("Answer added with id {anserId} to question {questionId}", answer.Id, command.QuestionId);

        return answer.Id;
    }
}