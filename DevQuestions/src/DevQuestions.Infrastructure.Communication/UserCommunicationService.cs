using CSharpFunctionalExtensions;
using DevQuestions.Application.Communication;
using Shared;

namespace DevQuestions.Infrastructure.Communication;

public class UsersCommunicationService: IUsersCommunicationService
{
    public Task<Result<long, Failure>> GetUserRating(Guid userId, CancellationToken cancellationToken = default) 
        => throw new NotImplementedException();
}