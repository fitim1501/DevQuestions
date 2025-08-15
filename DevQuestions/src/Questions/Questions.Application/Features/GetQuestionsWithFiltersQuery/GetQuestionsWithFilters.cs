using DevQuestions.Application.Abstractions;
using DevQuestions.Application.FilesStorage;
// using DevQuestions.Application.Tags;
using DevQuestions.Contracts.Questions.Dtos;
using DevQuestions.Contracts.Questions.Responses;
using DevQuestions.Domain.Questions;
using Microsoft.EntityFrameworkCore;

namespace DevQuestions.Application.Questions.Features.GetQuestionsWithFiltersQuery;

public class GetQuestionsWithFilters : IQueryHandler<QuestionResponse, GetQuestionsWithFiltersQuery>
{
    private readonly IFilesProvider _filesProvider;
    //private readonly ITagsReadDbContext _tagsReadDbContext;
    private readonly IQuestionsReadDbContext _questionsReadDbContext;

    public GetQuestionsWithFilters(
        IFilesProvider filesProvider,
        //ITagsReadDbContext tagsReadDbContext,
        IQuestionsReadDbContext questionsReadDbContext)
    {
        _filesProvider = filesProvider;
        //_tagsReadDbContext = tagsReadDbContext;
        _questionsReadDbContext = questionsReadDbContext;
    }

    public async Task<QuestionResponse> Handle(
        GetQuestionsWithFiltersQuery query,
        CancellationToken cancellationToken)
    {
        var questions = await _questionsReadDbContext.ReadQuestions
            .Include(q => q.Solution)
            .Skip((query.Dto.Page - 1) * query.Dto.PageSize)
            .Take(query.Dto.PageSize)
            .ToListAsync(cancellationToken);

        long count = await _questionsReadDbContext.ReadQuestions.LongCountAsync(cancellationToken);

        var screenshotIds = questions
            .Where(q => q.ScreenshotId is not null)
            .Select(q => q.ScreenshotId!.Value);

        var filesDict = await _filesProvider.GetUrlsByIdAsync(screenshotIds, cancellationToken);

        var questionTags = questions
            .SelectMany(q => q.Tags);

        // var tags = await _tagsReadDbContext.TagsRead
        //     .Where(t => questionTags.Contains(t.Id))
        //     .Select(t => t.Name)
        //     .ToListAsync(cancellationToken);

        var questionsDto = questions.Select(p => new QuestionDto(
            p.Id,
            p.Title,
            p.Text,
            p.UserId,
            p.ScreenshotId is not null ? filesDict[p.ScreenshotId.Value] : null,
            p.Solution?.Id,
            [],
            p.Status.ToRussianString()));

        return new QuestionResponse(questionsDto, count);
    }
}