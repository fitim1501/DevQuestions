﻿using DevQuestions.Domain.Questions;

namespace DevQuestions.Application.Questions;

public interface IQuestionsRepository
{
    Task<Guid> AddAsync(Question question, CancellationToken cancellationToken);

    Task<Guid> SaveAsync(Question question, CancellationToken cancellationToken);
    

    Task<Guid> DeleteAsync(Guid questionId, CancellationToken cancellationToken);

    Task<Guid> GetByIdAsync(Guid questionId, CancellationToken cancellationToken);

    Task<int> GetUnresolvedUserQuestionsAsync(Guid userId, CancellationToken cancellationToken);
}
