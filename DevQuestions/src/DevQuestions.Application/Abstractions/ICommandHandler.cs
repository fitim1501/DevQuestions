namespace DevQuestions.Application.Abstractions;

public interface ICommandHandler
{
    Task<Result<Guid, Failure>> Handle(CreateQuestionCommand command, CancellationToken cancellationToken)
}