using System.Text.Json.Serialization;

namespace ReactiveWeather.Models
{
    public class SevenDayForecast
    {
        public int Altitude { get; set; }
        [JsonPropertyName ("city_name")] public string CityName { get; set; }
        public Current Current { get; set; }
        [JsonPropertyName ("weather_symbol_id")] public int WeatherSymbolId { get; set; }
        [JsonPropertyName ("location_id")] public string LocationId { get; set; }
        List<Forecast> Forecasts { get; set; }
        public int Timestamp { get; set; }
    }
}