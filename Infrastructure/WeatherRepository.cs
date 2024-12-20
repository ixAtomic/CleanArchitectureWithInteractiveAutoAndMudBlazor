using Application.Interfaces;
using Domain;
using Domain.Models;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure;
public class WeatherRepository : IWeatherRepository
{
    private readonly IDbConnection _weatherDbConnection;
    public WeatherRepository(IDictionary<string, IDbConnection> connections, IOptions<ConnectionStrings> connectionStrings)
    {
        _weatherDbConnection = connections[nameof(connectionStrings.Value.WeatherDb)]; //use this connection if you have one
    }

    private readonly List<string> summaries = new()
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    public async Task<IEnumerable<WeatherForecast>> GetWeatherForecastsAsync()
    {
        // This is where you would normally query a database or call an API
        // to get the weather forecasts. For the sake of this example, we'll
        // just return a hard-coded list of WeatherForecast objects.
        var rng = new Random();
        await Task.Delay(1000); // Simulate a 1 second delay
        return await Task.FromResult(Enumerable.Range(1, 5).Select(index => new WeatherForecast(DateOnly.FromDateTime(DateTime.Now.AddDays(index)), rng.Next(-20, 55), summaries[rng.Next(summaries.Count)], "Super secret thing")));
    }
}
