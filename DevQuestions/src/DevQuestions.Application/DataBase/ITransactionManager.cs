using System.Data;

namespace DevQuestions.Application.DataBase;

public interface ITransactionManager
{
    Task<IDbTransaction> BeginTransactionAsync(CancellationToken cancellationToken);
}