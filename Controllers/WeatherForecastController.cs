using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using project5.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace project5.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Cool", "Warm", "Hot"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IWeatherForecastService _weatherForecastService;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IWeatherForecastService weatherForecastService)
        {
            _logger = logger;
            _weatherForecastService = weatherForecastService;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get(string city, string country)
        {
            return _weatherForecastService.GetWeatherForecasts(city, country);
        }

        [HttpPost]
        public ActionResult<WeatherForecast> Post(WeatherForecast forecast)
        {
            return _weatherForecastService.AddWeatherForecast(forecast);
        }
    }
}
