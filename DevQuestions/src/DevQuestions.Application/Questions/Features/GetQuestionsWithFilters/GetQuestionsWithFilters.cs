using CSharpFunctionalExtensions;
using DevQuestions.Application.Abstractions;
using DevQuestions.Application.FilesStorage;
using DevQuestions.Application.Tags;
using DevQuestions.Contracts.Questions.Dtos;
using DevQuestions.Contracts.Questions.Responses;
using DevQuestions.Domain.Questions;
using Shared;

namespace DevQuestions.Application.Questions.Features.GetQuestionsWithFilters;

public class GetQuestionsWithFilters : IHandler<QuestionResponse, GetQuestionsWithFiltersCommand>
{
    private readonly IQuestionsRepository _questionsRepository;
    private readonly IFilesProvider _filesProvider;
    private readonly ITagsRepository _tagsRepository;
    private readonly QuestionsDbContext _dbContext;

    public GetQuestionsWithFilters(IQuestionsRepository questionsRepository, IFilesProvider filesProvider, ITagsRepository tagsRepository)
    {
        _questionsRepository = questionsRepository;
        _filesProvider = filesProvider;
        _tagsRepository = tagsRepository;
    }

    public async Task<Result<QuestionResponse, Failure>> Handle(
        GetQuestionsWithFiltersCommand command,
        CancellationToken cancellationToken)
    {
        (IReadOnlyList<Question> questions, long count) = await _questionsRepository.GetQuestionsWithFiltersAsync(command, cancellationToken);

        var screenshotIds = questions
            .Where(q => q.ScreenshotId is not null)
            .Select(q => q.ScreenshotId!.Value);

        var filesDict = await _filesProvider.GetUrlsByIdAsync(screenshotIds, cancellationToken);

        var questionTags = questions
            .SelectMany(q => q.Tags);

        var tags = await _tagsRepository.GetTagsAsync(questionTags, cancellationToken);

        var questionsDto = questions.Select(p => new QuestionDto(
            p.Id,
            p.Title,
            p.Text,
            p.UserId,
            p.ScreenshotId is not null ? filesDict[p.ScreenshotId.Value] : null,
            p.Solution?.Id,
            tags,
            p.Status.ToRussianString()));

        return new QuestionResponse(questionsDto, count);
    }
}