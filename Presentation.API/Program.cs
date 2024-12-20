using Application;
using Application.Public.Interfaces;
using Domain;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

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

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

var app = builder.Build();

app.UseCors();

app.MapDefaultEndpoints();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();



app.MapGet("/weatherforecast", async ([FromServices] IWeatherService service) =>
{
    return await service.GetWeatherForecastsAsync();
})
.WithName("GetWeatherForecast");

app.Run();
