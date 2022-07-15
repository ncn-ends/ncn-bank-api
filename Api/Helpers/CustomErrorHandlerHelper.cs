using System.Diagnostics;
using System.Net;
using System.Net.Mime;
using System.Security;
using System.Security.Authentication;
using System.Text.Json;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace Api.Helpers;

public static class CustomErrorHandlerHelper
{
    public static void UseCustomExceptionHandler(this WebApplication app)
    {
        app.UseExceptionHandler(exceptionHandler =>
        {
            exceptionHandler.Run(async context =>
            {
                if (app.Environment.IsDevelopment()) await WriteResponse(context, includeDetails: true);
                else await WriteResponse(context, includeDetails: false);
            });
        });
    }

    private static async Task WriteResponse(HttpContext context, bool includeDetails)
    {
        /* Try to retrieve error from ExceptionHandler middleware */
        var exceptionDetails = context.Features.Get<IExceptionHandlerFeature>();
        var err = exceptionDetails?.Error;

        if (err is null) return;

        var title = includeDetails ? "An error occured: " + err.Message : "An error occured";
        var details = includeDetails ? err.ToString() : null;
        var status = err switch
        {
            ArgumentException => 404,
            _ => 500
        };

        var problem = new ProblemDetails
        {
            Status = status,
            Title = title,
            Detail = details
        };

        var traceId = Activity.Current?.Id ?? context?.TraceIdentifier;
        if (traceId != null)
        {
            problem.Extensions["tradeId"] = traceId;
        }
        
        if (context is null) return;
        
        /* ProblemDetails has it's own content type */
        context.Response.ContentType = "application/problem+json";
        context.Response.StatusCode = status;
        
        await context.Response.WriteAsJsonAsync(problem);
    }
}