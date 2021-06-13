using Newtonsoft.Json;

namespace ReactiveWeather.Models
{
    public class Current
    {
        public string Temperature { get; set; }
        [JsonProperty(PropertyName = "weather_symbol_id")] public string WeatherSymbolId { get; set; }
    }
}