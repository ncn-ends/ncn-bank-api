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
        IAccountTypeAccess access,
        [FromBody] AccountFormDTO accountInfoForm
    )
    {
        // TODO: validate that accountInfoForm has no null fields
        return Results.Ok();
    }
}