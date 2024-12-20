using Application.Public.Interfaces;
using Domain.Public;
using System.Net.Http.Json;

namespace Presentation.Client.Services;

public class PublicWeatherService : IWeatherService
{
    private readonly HttpClient _httpClient;

    public PublicWeatherService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<IEnumerable<WeatherResponse>> GetWeatherForecastsAsync()
    {
        var response = await _httpClient.GetFromJsonAsync<IEnumerable<WeatherResponse>>("weatherforecast");
        return response ?? [];
    }
}
