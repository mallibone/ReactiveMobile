using System.Collections.Generic;
using Newtonsoft.Json;

namespace ReactiveWeather.Models
{
    public class SevenDayForecast
    {
        public int Altitude { get; set; }
        [JsonProperty(PropertyName = "city_name")] public string CityName { get; set; }
        public Current Current { get; set; }
        [JsonProperty(PropertyName = "weather_symbol_id")] public int WeatherSymbolId { get; set; }
        [JsonProperty(PropertyName = "location_id")] public string LocationId { get; set; }
        List<Forecast> Forecasts { get; set; }
        public int Timestamp { get; set; }
    }
}