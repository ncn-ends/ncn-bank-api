using System.Diagnostics;
using DataAccess.Access;
using DataAccess.Models;
using Microsoft.AspNetCore.Mvc;

namespace Api.Endpoints;

public static class AccountEndpoints
{
    public static void MapAccountEndpoints(this WebApplication app)
    {
        app.MapPost("/api/account", CreateNewAccount);
        app.MapGet("/api/account/{accountId}", GetAccountById);
    }

    private static async Task<IResult> CreateNewAccount(
        IAccountAccess access,
        [FromBody] AccountFormDTO accountInfoForm
    )
    {
        var createdAccount = await access.CreateOne(accountInfoForm);
        if (createdAccount is null) return Results.BadRequest();
        return Results.Ok(createdAccount);
    }
    
    private static async Task<IResult> GetAccountById(
        Guid accountId,
        IAccountAccess access
    )
    {
        var fetchedAccount = await access.GetOne(accountId);
        if (fetchedAccount is null) return Results.BadRequest();
        return Results.Ok(fetchedAccount);
    }
}