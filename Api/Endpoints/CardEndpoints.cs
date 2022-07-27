using DataAccess.Access;
using DataAccess.Models;
using Microsoft.AspNetCore.Mvc;

namespace Api.Endpoints;

public static class CardEndpoints
{
    public static void MapCardEndpoints(this WebApplication app)
    {
        app.MapPost("/api/card", CreateOne);
    }

    private async static Task<IResult> CreateOne(
        ICardAccess cardAccess,
        [FromBody] CardCreationForm cardForm

    )
    {
        var createdCard = await cardAccess.CreateOne(cardForm);
        if (createdCard is null) return Results.BadRequest();
        return Results.Ok(createdCard);
    }
}