using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using ReactiveWeather.Models;

namespace ReactiveWeather.Views
{
    public class WeatherForecastViewModel : ReactiveObject
    {
        public WeatherForecastViewModel(SevenDayForecast forecast)
        {
            CityName = forecast.CityName;
        }

        [Reactive] public string CityName { get; set; }
    }
}