using System;
using System.IO;
using System.Net.Http;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ReactiveWeather.Models;
using Splat;
using WebApplication;

namespace ReactiveWeather.Services
{
    public class WeatherService
    {
        private readonly HttpClient _httpClient;
        // private const string BackendUrl = "https://asdf/{0}";
        private const string BackendUrl = "https://dwxweatherforecast.azurewebsites.net/api/weatherforecast/forpostalcode/{0}";

        public WeatherService()
        {
            _httpClient = new HttpClient();
        }
        // public IObservable<SevenDayForecast> GetSevenDayForecast(int postalCode) => Observable.Return(JsonConvert.DeserializeObject<WeekForecastRoot>(DummyData)?.Data);

        public IObservable<WeatherForecast> GetWeatherForecast(int postalCode)
        {
            var url = string.Format(BackendUrl, postalCode);
            return 
                Observable.StartAsync(() => _httpClient.GetStringAsync(url))
                .Select(JsonConvert.DeserializeObject<WeatherForecast>);
        }
    }
}