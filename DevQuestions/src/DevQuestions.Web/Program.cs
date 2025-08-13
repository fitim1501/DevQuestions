using DevQuestions.Application.Communication;
using DevQuestions.Infrastructure.Communication;
using DevQuestions.Web;
using DevQuestions.Web.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IUsersCommunicationService, UsersCommunicationService>();
builder.Services.AddProgramDependencies();

var app = builder.Build();

app.UseExceptionMiddleware();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI(option => option.SwaggerEndpoint("/openapi/v1.json", "DevQuestions"));
}

app.MapControllers();

app.Run();