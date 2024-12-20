namespace Domain.Public;

public record WeatherResponse(DateOnly Date, int TemperatureC, int TemperatureF, string? Summary);
