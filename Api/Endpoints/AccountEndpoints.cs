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
    }

    private static async Task<IResult> CreateNewAccount(
        IAccountAccess access,
        [FromBody] AccountFormDTO accountInfoForm
    )
    {
        Debugger.Break();
        var createdAccount = await access.CreateOne(accountInfoForm);
        if (createdAccount is null) return Results.BadRequest();
        return Results.Ok(createdAccount);
    }
}