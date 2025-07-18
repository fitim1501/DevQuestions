using DevQuestions.Application.Questions;
using DevQuestions.Domain.Questions;

namespace DevQuestion.Infrastructure.Postgres.Repositories;

public class QuestionsSqlRepository : IQuestionsRepository
{
    public async Task<Guid> AddAsync(Question question, CancellationToken cancellationToken)
    {
        
    }

    public async Task<Guid> SaveAsync(Question question, CancellationToken cancellationToken)
    {
        
    }

    public Task<Guid> DeleteAsync(Guid questionId, CancellationToken cancellationToken)
    {
        
    }

    public Task<Guid> GetByIdAsync(Guid questionId, CancellationToken cancellationToken)
    {
        
    }

    public Task<int> GetUnresolvedUserQuestionsAsync(Guid userId, CancellationToken cancellationToken)
    {
        
    }
}