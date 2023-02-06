using System;
using System.Net.Http;
using System.Reactive.Linq;
using System.Text.Json;
using Akavache;
using ReactiveWeather.Models;

namespace ReactiveWeather.Services
{
    public class WeatherService
    {
        private readonly HttpClient _httpClient;
        private const string BackendUrl = "https://mauireactivewebapi.azurewebsites.net/api/weatherforecast/forpostalcode/{0}";

        public WeatherService()
        {
            _httpClient = new HttpClient();
        }

        private IObservable<WeatherForecast> GetWeatherForecast(int postalCode)
        {
            var url = string.Format(BackendUrl, postalCode);
            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            };
            return 
                Observable.StartAsync(() => _httpClient.GetStringAsync(url))
                .Select(forecast => JsonSerializer.Deserialize<WeatherForecast>(forecast, options)!);
        }
        
        public IObservable<WeatherForecast> GetWeatherForecastCache(int postalCode)
        {
            return
                // No Cache
                GetWeatherForecast(postalCode);
            
                // Expiration Cache (the classic)
                // BlobCache.InMemory.GetOrFetchObject(
                //     $"forecast{postalCode}",
                //     () => GetWeatherForecast(postalCode),
                //     DateTimeOffset.Now.AddSeconds(5))!;
                
                // Get and fetch latest
                // BlobCache.InMemory.GetAndFetchLatest($"forecast{postalCode}",
                //     () => GetWeatherForecast(postalCode))!;
        }
    }
}