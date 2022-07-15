using Api.Endpoints;
using Api.Helpers;
using DataAccess;
using DataAccess.Placeholder;

var builder = WebApplication.CreateBuilder();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<ISqlDataAccess, SqlDataAccess>();
builder.Services.AddSingleton<IPersonsAccess, PersonsAccess>();
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
// app.ConfigureBuildsEndpoints();

app.Run();