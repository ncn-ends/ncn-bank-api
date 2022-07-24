using DataAccess.Access;

namespace Api.Endpoints;

public static class AccountTypeEndpoints
{
    public static void MapAccountTypeEndpoints(this WebApplication app)
    {
        app.MapGet("/api/accounttypes", GetAll);
    }

    private static async Task<IResult> GetAll(IAccountTypeAccess access)
    {
        var res = await access.GetAllAccountTypes();
        return Results.Ok(res);
    }
}