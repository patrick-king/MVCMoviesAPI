using Microsoft.AspNetCore.Mvc;
using MVCMoviesAPI.ViewModels;

namespace MVCMoviesAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }

        //[HttpGet(Name = "GetWeatherForecastToday")]
        //public WeatherForecastViewModel Get(string city)
        //{
        //    var date = DateTime.Now;
            
        //    var temps = new List<Int64>();
        //    for (int i = 0;i<24;i++)
        //    {
        //        temps.Add(Random.Shared.Next(-40, 100));
        //    }

        //    return new WeatherForecastViewModel {
        //        Date = date,
        //        City = city,
        //        HourlyTemperature = temps.ToArray()
        //    };
        //}

        //[HttpGet(Name = "GetWeatherForecast2")]
        //public IEnumerable<WeatherForecastV2> GetCity(string city)
        //{
        //    return Enumerable.Range(1, 5).Select(index => new WeatherForecastV2
        //    {
        //        Date = DateTime.Now.AddDays(index),
        //        TemperatureC = Random.Shared.Next(-20, 55),
        //        Summary = Summaries[Random.Shared.Next(Summaries.Length)],
        //        City = city

        //    })
        //    .ToArray();
        //}
    }

    public class WeatherForecastViewModel
    {
        public string City{ get; set; }
        public Int64[] HourlyTemperature { get; set; }
        public DateTime Date { get; set; }
    }
}