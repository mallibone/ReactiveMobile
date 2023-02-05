using System.Text.Json.Serialization;

namespace ReactiveWeather.Models
{
    public class Forecast
    {
        [JsonPropertyName("temp_high")] public string TempHigh { get; set; }
        [JsonPropertyName("temp_low")] public string TempLow { get; set; }
        [JsonPropertyName("weather_symbol_id")]public string WeatherSymbolId { get; set; }
        public string Day { get; set; }
    }
}