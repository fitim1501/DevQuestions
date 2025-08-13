using System.Data;
using DevQuestions.Application.DataBase;

namespace DevQuestion.Infrastructure.Postgres;

public class TransactionManager : ITransactionManager
{
    public Task<IDbTransaction> BeginTransactionAsync(CancellationToken cancellationToken) => throw new NotImplementedException();
}