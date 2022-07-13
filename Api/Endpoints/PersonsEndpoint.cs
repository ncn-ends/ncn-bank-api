using DataAccess.Placeholder;

namespace Api.Endpoints;

public static class PersonsEndpoint
{
    public static void ConfigurePersonsEndpoints(this WebApplication app)
    {
        app.MapGet("/api/persons", GetPersons);
    }

    private static async Task<IResult> GetPersons(IPersonsAccess personsAccess)
    {        
        try
        {
            return Results.Ok(await personsAccess.GetPersons());
        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
            return Results.Problem(e.Message);
        }
    }
}