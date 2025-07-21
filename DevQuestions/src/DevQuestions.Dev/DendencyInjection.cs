using DevQuestions.Application;
using DevQuestions.Application.Questions;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace DevQuestions.Dev;

public static class DendencyInjection
{
    public static IServiceCollection AddProgramDependencies(this IServiceCollection services) =>
        services
            .AddWebDependencies()
            .AddApplication();
    private static IServiceCollection AddWebDependencies(this IServiceCollection services)
    {
        services.AddControllers();
        services.AddOpenApi();

        return services;
    }
}