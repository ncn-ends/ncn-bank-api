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

    private static async Task<IResult> AddAddress(AddressInsertionForm address, IAddressAccess addressAccess)
    {
        var address_id = await addressAccess.AddAddress(address);
        return Results.Ok(new {address_id});
    }
}