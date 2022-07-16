using System.ComponentModel.DataAnnotations;
using DataAccess.Access;
using DataAccess.Models;
using LanguageExt.Common;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Api.Endpoints;

public static class AddressEndpoints
{
    public static void MapAddressEndpoints(this WebApplication app)
    {
        app.MapPost("/api/address", AddAddress);
    }

    private static async Task<IResult> AddAddress(AddressDTO address)
    {
        return Results.Ok(address);
    }
}