namespace DevQuestion.Infrastructure.Postgres.Seeders;

public class QuestionsSeeder : ISeeder
{
    private readonly QuestionsReadDbContext _context;

    public QuestionsSeeder(QuestionsReadDbContext context)
    {
        _context = context;
    }

    public Task SeedAsync()
    {
        throw new NotImplementedException();
    }
}