namespace DevQuestions.Application.Abstractions;
public interface IQuery;

public interface IQueryHandler<TResponse, in TQuery>
    where TQuery : IQuery
{
    Task<TResponse> Handle(TQuery command, CancellationToken cancellationToken);
}