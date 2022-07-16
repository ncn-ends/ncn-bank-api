using System.ComponentModel.DataAnnotations;
using DataAccess.Access;
using DataAccess.Models;
using LanguageExt.Common;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Api.Endpoints;

public static class PersonsEndpoint
{
    public static void MapPersonsEndpoints(this WebApplication app)
    {
        app.MapGet("/api/persons", GetPersons);
        app.MapPost("/api/person", CreatePerson);
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

    private static async Task<IResult> CreatePerson(PersonBO person, IPersonManager personManager)
    {
        await personManager.Manage(person);
        return Results.Ok(person);
    }
}