using System.Text.Json.Serialization;

namespace ReactiveWeather.Models
{
    public class Current
    {
        public string Temperature { get; set; }
        [JsonPropertyName("weather_symbol_id")] public string WeatherSymbolId { get; set; }
    }
}