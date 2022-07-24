using Api.Endpoints;
using Api.Extensions;
using Api.Helpers;
using DataAccess;
using DataAccess.Access;
using DataAccess.Models;
using FluentValidation;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDataAccess();

builder.Services.AddScoped<IValidator<PersonBO>, PersonValidator>();
builder.Services.AddScoped<IPersonManager, PersonManager>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var setupAccess = services.GetRequiredService<ISetupAccess>();
    await setupAccess.EnsureDatabaseSetup();
}


/* Middleware Pipeline */
app.UseCustomExceptionHandler();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// MappingRules.SetRules();
app.UseHttpsRedirection();

app.MapEndpoints();

app.Run();
