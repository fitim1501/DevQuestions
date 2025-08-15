namespace DevQuestions.Web.Features;

public interface IEndpoint
{
    void MapEndPoint(IEndpointRouteBuilder endpoints);
}

public class Create : IEndpoint
{
    public void MapEndPoint()
    {
        
    }
}