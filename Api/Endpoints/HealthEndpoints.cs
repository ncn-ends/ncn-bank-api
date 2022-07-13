namespace Api.Endpoints;

public static class HealthEndpoints
{
    public static void ConfigureHealthEndpoints(this WebApplication app)
    {
        app.MapGet("/ping", Ping);
    }

    private static IResult Ping()
    {
        try
        {
            return Results.Ok("pong");
        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
            return Results.Problem(e.Message);
        }
    }
}