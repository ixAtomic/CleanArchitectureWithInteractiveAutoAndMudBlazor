using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models;

public record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary, string SuperSecretThingThatWeDontWantToBePublic)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
