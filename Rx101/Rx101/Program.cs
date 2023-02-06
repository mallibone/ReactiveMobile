using System;
using System.Net.Http;
using System.Reactive.Linq;
using System.Text.Json;

namespace Rx101
{
    class Program
    {
        static void Main(string[] _)
        {
            // Demo01.SimpleComparison();
            // Demo02.FilterEventFlows();
            // Demo03.MergeFlows();
            // Demo04.SimulateAsyncCalls();
            // Demo05();
            Gnabber();
            Console.ReadLine();
        }

        private static void Gnabber()
        {
            var url = "https://mauireactivewebapi.azurewebsites.net/api/weatherforecast/forpostalcode/8200";
            var httpClient = new HttpClient();
            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            };
            Observable.StartAsync(() => httpClient.GetStringAsync(url))
                .Select(forecast => JsonSerializer.Deserialize<WeatherForecast>(forecast, options)!)
                .Subscribe(forecast => Console.WriteLine(forecast.Summary));
        }

        private static void Demo05()
        {
            var demo05 = new Demo05();
            Console.ReadLine();
            demo05.Dispose();
        }

    }

        public class WeatherForecast
        {
            public DateTime Date { get; set; }
            public float TemperatureC { get; set; }
            public float TemperatureF { get; set; }
            public int Windspeed { get; set; } // kph
            public int Humidity { get; set; } // %
            public string Summary { get; set; } = string.Empty;
            public int PostalCode { get; set; }
        }
}