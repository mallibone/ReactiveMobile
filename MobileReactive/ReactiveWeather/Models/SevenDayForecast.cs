using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ReactiveWeather.Models
{
    public class SevenDayForecast
    {
        public int Altitude { get; set; }
        [JsonPropertyName ("city_name")] public string CityName { get; set; } = string.Empty;
        public Current Current { get; set; } = new ();
        [JsonPropertyName ("weather_symbol_id")] public int WeatherSymbolId { get; set; }
        [JsonPropertyName ("location_id")] public string LocationId { get; set; } = string.Empty;
        List<Forecast> Forecasts { get; set; } = new ();
        public int Timestamp { get; set; }
    }
}