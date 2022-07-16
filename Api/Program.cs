using Api.Endpoints;
using Api.Helpers;
using DataAccess;
using DataAccess.Access;
using DataAccess.Models;
using FluentValidation;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<ISqlDataAccess, SqlDataAccess>();
builder.Services.AddSingleton<IPersonsAccess, PersonsAccess>();

builder.Services.AddScoped<IValidator<PersonBO>, PersonValidator>();
builder.Services.AddScoped<IPersonManager, PersonManager>();

var app = builder.Build();

app.UseCustomExceptionHandler();

/* Middleware Pipeline */
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// MappingRules.SetRules();
app.UseHttpsRedirection();

app.MapHealthEndpoints();
app.MapPersonsEndpoints();
app.MapAddressEndpoints();

app.Run();
