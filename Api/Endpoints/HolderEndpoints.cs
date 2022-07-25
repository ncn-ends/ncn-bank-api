using System.Diagnostics;
using DataAccess.Access;
using DataAccess.Models;
using Microsoft.AspNetCore.Mvc;

namespace Api.Endpoints;

public static class HolderEndpoints
{
    public static void MapHolderEndpoints(this WebApplication app)
    {
        app.MapPost("/api/holder", CreateAccountHolder);
        app.MapGet("/api/holder/{holderId}", GetAccountHolderById);
    }

    private static async Task<IResult> CreateAccountHolder(
        IAccountHolderAccess holderAccess,
        HttpRequest req, 
        [FromBody] AccountHolderDTO holderDto
    )
    {
        var insertedId = await holderAccess.CreateOne(holderDto);
        
        if (insertedId is null) Results.BadRequest();
        
        return Results.Ok(new
        {
            account_holder_id = insertedId    
        });
    }

    private static async Task<IResult> GetAccountHolderById(
        IAccountHolderAccess holderAccess,
        string holderId
    )
    {
        var holder = await holderAccess.GetOne((Guid.Parse((ReadOnlySpan<char>) holderId)));
        if (holder is null) return Results.BadRequest();
        return Results.Ok(holder);
    }
}