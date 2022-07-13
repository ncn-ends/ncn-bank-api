using Api.Endpoints;

var builder = WebApplication.CreateBuilder();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// builder.Services.AddSingleton<ISqlDataAccess, SqlDataAccess>();
// builder.Services.AddSingleton<IBuildsDataAccess, BuildsDataAccessAccess>();
var app = builder.Build();

/* Middleware Pipeline */
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// MappingRules.SetRules();
app.UseHttpsRedirection();

app.ConfigureHealthEndpoints();
// app.ConfigureBuildsEndpoints();

app.Run();