using System.Collections.Generic;
using project5.Models;

namespace project5
{
    public interface IWeatherForecastService
    {
        IEnumerable<WeatherForecast> GetWeatherForecasts(string city, string country);
        WeatherForecast AddWeatherForecast(WeatherForecast forecast);
    }
}
