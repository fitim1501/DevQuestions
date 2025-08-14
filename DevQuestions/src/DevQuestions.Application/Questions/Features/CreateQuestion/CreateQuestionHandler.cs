using CSharpFunctionalExtensions;
using DevQuestions.Application.Abstractions;
using DevQuestions.Application.Extensions;
using DevQuestions.Application.Questions.Fails;
using DevQuestions.Contracts.Questions;
using DevQuestions.Contracts.Questions.Dtos;
using DevQuestions.Domain.Questions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using Shared;

namespace DevQuestions.Application.Questions.Features.CreateQuestion;

public class CreateQuestionHandler : IHandler<Guid,  CreateQuestionCommand>
{
    private readonly ILogger<CreateQuestionHandler> _logger;
    private readonly IQuestionsRepository _questionsRepository;
    private readonly IValidator<CreateQuestionDto> _validator;
    public CreateQuestionHandler(
        ILogger<CreateQuestionHandler> logger,
        IValidator<CreateQuestionDto> validator,
        IQuestionsRepository questionsRepository)
    {
        _logger = logger;
        _validator = validator;
        _questionsRepository = questionsRepository;
    }
    public async Task<Result<Guid, Failure>> Handle(CreateQuestionCommand command, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(command.QuestionDto, cancellationToken);
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
            .GetOpenUserQuestionsAsync(command.QuestionDto.UserId, cancellationToken);

        var existedQuestion = await _questionsRepository.GetByIdAsync(Guid.Empty, cancellationToken);

        if (openUserQuestionCount > 3)
        {
            return Errors.Questions.ToManyQuestions().ToFailure();
        }

        var questionId = Guid.NewGuid();

        var question = new Question(
            questionId,
            command.QuestionDto.Title,
            command.QuestionDto.Text,
            command.QuestionDto.UserId,
            null,
            command.QuestionDto.TagIds);

        await _questionsRepository.AddAsync(question, cancellationToken);

        _logger.LogInformation("Question created with id {questionId}", questionId);

        return questionId;
    }
}