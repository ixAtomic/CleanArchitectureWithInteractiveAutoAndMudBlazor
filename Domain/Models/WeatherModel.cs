namespace Domain.Models;

public record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary, string SuperSecretThingThatWeDontWantToBePublic)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
