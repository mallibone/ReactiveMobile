using System.Text.Json.Serialization;

namespace ReactiveWeather.Models
{
    public class Forecast
    {
        [JsonPropertyName("temp_high")] public string TempHigh { get; set; } = string.Empty;
        [JsonPropertyName("temp_low")] public string TempLow { get; set; } = string.Empty;
        [JsonPropertyName("weather_symbol_id")]public string WeatherSymbolId { get; set; } = string.Empty;
        public string Day { get; set; } = string.Empty;
    }
}