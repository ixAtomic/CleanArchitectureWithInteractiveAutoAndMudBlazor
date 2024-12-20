using Application;
using Application.Public.Interfaces;
using Domain;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Data.Common;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddInfrastructure()
    .AddApplication();

var ConnectionStringConfig = builder.Configuration.GetRequiredSection("ConnectionStrings");
var ConnectionStrings = ConnectionStringConfig.Get<ConnectionStrings>() ?? throw new Exception("Section must exist");

builder.Services.AddSingleton<IDictionary<string, IDbConnection>>((x) =>
{
    return new Dictionary<string, IDbConnection>()
    {
        { nameof(ConnectionStrings.WeatherDb), new SqlConnection(ConnectionStrings.WeatherDb) },
        { nameof(ConnectionStrings.OtherDb), new SqlConnection(ConnectionStrings.OtherDb) }
    };
});

builder.Services.Configure<ConnectionStrings>(ConnectionStringConfig);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();



app.MapGet("/weatherforecast", ([FromServices] IWeatherService service) =>
{
    return service.GetWeatherForecastsAsync();
})
.WithName("GetWeatherForecast");

app.Run();
