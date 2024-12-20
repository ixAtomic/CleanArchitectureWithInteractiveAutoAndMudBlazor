using Domain.Public;

namespace Application.Public.Interfaces;

public interface IWeatherService
{
    Task<IEnumerable<WeatherResponse>> GetWeatherForecastsAsync();
}
