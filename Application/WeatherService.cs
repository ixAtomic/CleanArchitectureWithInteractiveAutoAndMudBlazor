using Application.Interfaces;
using Application.Public.Interfaces;
using Domain.Public;

namespace Application;
public class WeatherService : IWeatherService
{
    private readonly IWeatherRepository _weatherRepository;
    public WeatherService(IWeatherRepository weatherRepository)
    {
        _weatherRepository = weatherRepository;
    }

    public async Task<IEnumerable<WeatherResponse>> GetWeatherForecastsAsync()
    {
        var forecasts = await _weatherRepository.GetWeatherForecastsAsync();
        return forecasts.Select(forecast => new WeatherResponse(
                forecast.Date,
                forecast.TemperatureC,
                forecast.TemperatureF,
                forecast?.Summary
        ));
    }
}
