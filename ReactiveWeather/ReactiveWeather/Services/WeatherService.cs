using System;
using System.Reactive.Linq;
using Newtonsoft.Json;
using ReactiveWeather.Models;

namespace ReactiveWeather.Services
{
    public class WeatherService
    {
        private const string DummyData =
            "{\"data\":{\"altitude\":441,\"city_name\":\"Winterthur\",\"current\":{\"temperature\":\"19\",\"weather_symbol_id\":\"1\"},\"weather_symbol_id\":20,\"location_id\":\"840000\",\"forecasts\":[{\"temp_high\":\"27\",\"temp_low\":\"10\",\"weather_symbol_id\":\"1\",\"day\":\"Mon\"},{\"temp_high\":\"29\",\"temp_low\":\"12\",\"weather_symbol_id\":\"1\",\"day\":\"Tue\"},{\"temp_high\":\"30\",\"temp_low\":\"14\",\"weather_symbol_id\":\"1\",\"day\":\"Wed\"},{\"temp_high\":\"31\",\"temp_low\":\"15\",\"weather_symbol_id\":\"12\",\"day\":\"Thu\"},{\"temp_high\":\"30\",\"temp_low\":\"18\",\"weather_symbol_id\":\"12\",\"day\":\"Fri\"}],\"timestamp\":1623571200},\"config\":{\"name\":\"weather-widget\",\"language\":\"en\",\"version\":\"1.0.0\",\"timestamp\":1623572292}}";
        public IObservable<SevenDayForecast> GetSevenDayForecast(int postalCode) => Observable.Return(JsonConvert.DeserializeObject<WeekForecastRoot>(DummyData)?.Data);
    }
}