using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace WebApplication.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
                {
                    Date = DateTime.UtcNow.AddDays(index),
                    TemperatureC = rng.Next(-20, 55),
                    Humidity = rng.Next(40, 85),
                    Windspeed = rng.Next(4, 55),
                    Summary = Summaries[rng.Next(Summaries.Length)]
                })
                .ToArray();
        }
        
        [HttpGet("ForPostalcode/{postalCode}")]
        public WeatherForecast GetForPostalcode(int postalCode)
        {
            var rng = new Random();
            return new WeatherForecast
                {
                    PostalCode = postalCode,
                    Date = DateTime.UtcNow,
                    TemperatureC = rng.Next(17, 29),
                    Humidity = rng.Next(40, 85),
                    Windspeed = rng.Next(4, 55),
                    Summary = Summaries[rng.Next(Summaries.Length)]
                };
        }
    }
}