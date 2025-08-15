using DevQuestion.Infrastructure.Postgres.Questions;
using DevQuestions.Application.DataBase;
using DevQuestions.Application.Questions;
using Microsoft.Extensions.DependencyInjection;

namespace DevQuestion.Infrastructure.Postgres;

public static class DependencyInjection
{
    public static IServiceCollection AddPostgresInfrastructure(this IServiceCollection services)
    {
        services.AddDbContext<QuestionsReadDbContext>();
        services.AddScoped<IQuestionsRepository, QuestionsEfCoreRepository>();
        services.AddScoped<ITransactionManager, TransactionManager>();

        return services;
    }
}