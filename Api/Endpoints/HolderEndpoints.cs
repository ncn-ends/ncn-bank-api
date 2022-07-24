using System.Diagnostics;
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
        HttpRequest req, 
        [FromBody] AccountHolderDTO holderInfo
    ) {
        Debugger.Break();
        return Results.Ok();
    }

    private static async Task<IResult> GetAccountHolderById(string holderId)
    {
        return Results.Ok();
    }
}