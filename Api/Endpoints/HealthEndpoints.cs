using LanguageExt.Common;
using Microsoft.AspNetCore.Mvc;

namespace Api.Endpoints;

public static class HealthEndpoints
{
    public static void MapHealthEndpoints(this WebApplication app)
    {
        app.MapGet("/health/ping", HealthPing);
        app.MapGet("/health/problem", HealthProblem);
    }

    public static IResult HealthPing()
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

    public static IResult HealthProblem()
    {
        throw new ArgumentException();
    }
}