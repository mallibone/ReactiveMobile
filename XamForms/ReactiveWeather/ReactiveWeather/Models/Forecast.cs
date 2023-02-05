using Newtonsoft.Json;

namespace ReactiveWeather.Models
{
    public class Forecast
    {
        [JsonProperty(PropertyName = "temp_high")] public string TempHigh { get; set; }
        [JsonProperty(PropertyName = "temp_low")] public string TempLow { get; set; }
        [JsonProperty(PropertyName = "weather_symbol_id")]public string WeatherSymbolId { get; set; }
        public string Day { get; set; }
    }
}