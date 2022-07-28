using DataAccess.Access;
using DataAccess.Models;
using Microsoft.AspNetCore.Mvc;

namespace Api.Endpoints;

public static class CheckEndpoints
{
    public static void MapCheckEndpoints(this WebApplication app)
    {
        app.MapPost("/api/check", CreateOne);
    }

    private static async Task<IResult> CreateOne(
        ICheckAccess checkAccess,
        [FromBody] CheckCreationForm checkForm
    
    )
    {
        var createdCard = await checkAccess.CreateOne(checkForm);
        if (createdCard is null) return Results.BadRequest();
        return Results.Ok(createdCard);
    }
}