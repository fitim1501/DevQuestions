namespace DevQuestion.Infrastructure.Postgres.Questions;

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