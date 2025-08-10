using CSharpFunctionalExtensions;
using Dapper;
using DevQuestions.Application.DataBase;
using DevQuestions.Application.Questions;
using DevQuestions.Domain.Questions;
using Shared;

namespace DevQuestion.Infrastructure.Postgres.Repositories;

public class QuestionsSqlRepository : IQuestionsRepository
{
    private readonly ISqlConnectionFactory sqlConnectionFactory;

    public QuestionsSqlRepository(ISqlConnectionFactory sqlConnectionFactory)
    {
        this.sqlConnectionFactory = sqlConnectionFactory;
    }
    public async Task<Guid> AddAsync(Question question, CancellationToken cancellationToken)
    {
        const string sql = """
                            insert into questions (id, title, text, user_id, screenshot_id, tags, status)
                            values (@Id, @Title, @Text, @UserId, @ScreenshotId, @Tags, @Status)
                           """;

        using var connect = sqlConnectionFactory.Creat();

        await connect.ExecuteAsync(sql, new
        {
            Id = question.Id,
            Title = question.Title,
            Text = question.Text,
            UserId = question.UserId,
            ScreenshotId = question.ScreenshotId,
            Tags = question.Tags.ToArray(),
            Status = question.Status,
        });

        return question.Id;
    }

    public Task<Guid> SaveAsync(Question question, CancellationToken cancellationToken) => throw new NotImplementedException();

    public Task<Guid> DeleteAsync(Guid questionId, CancellationToken cancellationToken) => throw new NotImplementedException();

    public Task<Result<Question, Failure>> GetByIdAsync(Guid questionId, CancellationToken cancellationToken) => throw new NotImplementedException();

    public Task<int> GetOpenUserQuestionsAsync(Guid userId, CancellationToken cancellationToken) => throw new NotImplementedException();
    public Task<Guid> AddAnswerAsync(Answer answer, CancellationToken cancellationToken) => throw new NotImplementedException();
}