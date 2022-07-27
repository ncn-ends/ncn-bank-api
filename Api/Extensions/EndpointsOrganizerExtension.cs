using Api.Endpoints;

namespace Api.Extensions;

public static class EndpointsOrganizerExtension
{
    public static void MapEndpoints(this WebApplication app)
    {
        app.MapHealthEndpoints();
        app.MapPersonsEndpoints();
        app.MapAddressEndpoints();
        app.MapAccountTypeEndpoints();
        app.MapHolderEndpoints();
        app.MapAccountEndpoints();
        app.MapCardEndpoints();
    }
}