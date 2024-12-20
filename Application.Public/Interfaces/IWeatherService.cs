﻿using Domain.Public;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Public.Interfaces;

public interface IWeatherService
{
    Task<IEnumerable<WeatherResponse>> GetWeatherForecastsAsync();
}
