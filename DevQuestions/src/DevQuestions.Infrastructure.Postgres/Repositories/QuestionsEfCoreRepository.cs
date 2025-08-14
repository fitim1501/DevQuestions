using CSharpFunctionalExtensions;
using DevQuestions.Application.Questions;
using DevQuestions.Application.Questions.Fails;
using DevQuestions.Application.Questions.Features.GetQuestionsWithFilters;
using DevQuestions.Domain.Questions;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace DevQuestion.Infrastructure.Postgres.Repositories;

public class QuestionsEfCoreRepository : IQuestionsRepository
{
    private readonly QuestionsReadDbContext _readDbContext;

    public QuestionsEfCoreRepository(QuestionsReadDbContext readDbContext)
    {
        _readDbContext = readDbContext;
    }

    public async Task<Guid> AddAsync(Question question, CancellationToken cancellationToken)
    {
        await _readDbContext.Questions.AddAsync(question, cancellationToken);

        await _readDbContext.SaveChangesAsync(cancellationToken);

        return question.Id;
    }

    public async Task<Guid> SaveAsync(Question question, CancellationToken cancellationToken)
    {
        _readDbContext.Questions.Attach(question);
        await _readDbContext.SaveChangesAsync(cancellationToken);

        return question.Id;
    }

    public Task<Guid> DeleteAsync(Guid questionId, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<Result<Question, Failure>> GetByIdAsync(Guid questionId, CancellationToken cancellationToken)
    {
        var question = await _readDbContext.Questions
            .Include(g => g.Answers)
            .Include(q => q.Solution)
            .FirstOrDefaultAsync(p => p.Id == questionId, cancellationToken);

        if (question is null)
        {
            return Errors.General.NotFound(questionId).ToFailure();
        }

        return question;
    }

    public Task<(IReadOnlyList<Question> Questions, long count)> GetQuestionsWithFiltersAsync(GetQuestionsWithFiltersCommand command, CancellationToken cancellationToken) => throw new NotImplementedException();


    public Task<int> GetOpenUserQuestionsAsync(Guid userId, CancellationToken cancellationToken) => throw new NotImplementedException();
}