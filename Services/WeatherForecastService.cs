using System;
using System.Collections.Generic;
using System.Linq;
using project5.Models;

namespace project5
{
    public class WeatherForecastService : IWeatherForecastService
    {
        private readonly List<WeatherForecast> _weatherForecasts;

        public WeatherForecastService()
        {
            _weatherForecasts = new List<WeatherForecast>();
            InitializeWeatherForecasts();
        }

        private void InitializeWeatherForecasts()
        {
            var cities = new[] { "Wien", "Berlin", "London" };
            var countries = new[] { "Austria", "Germany", "United Kingdom" };
            var rng = new Random();

            for (int i = 0; i < cities.Length; i++)
            {
                var forecast = new WeatherForecast
                {
                    Date = DateTime.Now.AddDays(i),
                    TemperatureC = rng.Next(-20, 55),
                    Summary = Summaries[rng.Next(Summaries.Length)],
                    City = cities[i],
                    Country = countries[i]
                };

                _weatherForecasts.Add(forecast);
            }
        }

        public IEnumerable<WeatherForecast> GetWeatherForecasts(string city, string country)
        {
            var filteredForecasts = _weatherForecasts.Where(f => f.City == city && f.Country == country);
            return filteredForecasts;
        }

        public WeatherForecast AddWeatherForecast(WeatherForecast forecast)
        {
            _weatherForecasts.Add(forecast);
            return forecast;
        }

        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Cool", "Warm", "Hot"
        };
    }
}
