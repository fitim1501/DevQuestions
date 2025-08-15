using DevQuestion.Infrastructure.Postgres.Questions;
using Microsoft.Extensions.DependencyInjection;

namespace Questions.Infrastructure.Postgres;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddDbContext<QuestionsReadDbContext>();

        return services;
    }
}