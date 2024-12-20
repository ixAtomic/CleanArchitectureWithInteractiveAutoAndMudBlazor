using Domain.Models;

namespace Application.Interfaces;
public interface IWeatherRepository
{
    Task<IEnumerable<WeatherForecast>> GetWeatherForecastsAsync();
}
