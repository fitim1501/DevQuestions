namespace DevQuestions.Infrastructure.Postgres.Seeders;

public class QuestionsSeeder : ISeeder
{
    private readonly QuestionsDbContext _context;

    public QuestionsSeeder(QuestionsDbContext context)
    {
        _context = context;
    }

    public Task SeedAsync()
    {
        throw new NotImplementedException();
    }
}