using CSharpFunctionalExtensions;
using DevQuestions.Application.Abstractions;
using DevQuestions.Contracts.Questions.Responses;
using Shared;

namespace DevQuestions.Application.Questions.Features.GetQuestionsWithFilters;

public class GetQuestionsWithFilters : IHandler<QuestionResponse, GetQuestionsWithFiltersCommand>
{
    private readonly IQuestionsRepository _questionsRepository;

    public GetQuestionsWithFilters(IQuestionsRepository questionsRepository)
    {
        _questionsRepository = questionsRepository;
    }
    public Task<Result<QuestionResponse, Failure>> Handle(
        GetQuestionsWithFiltersCommand command,
        CancellationToken cancellationToken)
    {
        
    }
}