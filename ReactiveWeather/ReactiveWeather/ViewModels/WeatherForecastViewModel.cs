using System;
using System.Diagnostics;
using System.Reactive.Linq;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using ReactiveWeather.Services;
using ReactiveWeather.ViewModels;
using WebApplication;
using DateTime = System.DateTime;
using Exception = System.Exception;

namespace ReactiveWeather.Views
{
    public class WeatherForecastViewModel : ReactiveObject
    {
        private readonly LocationViewItem _location;
        private readonly WeatherService _weatherService;

        public WeatherForecastViewModel(LocationViewItem location)
        {
            _location = location;
            _weatherService = new WeatherService();
        }

        [Reactive] public bool IsBusy { get; set; }

        [Reactive] public string CityName { get; set; }
        [Reactive] public int Humidity { get; set; }
        [Reactive] public int Windspeed { get; set; }
        [Reactive] public float Temperature { get; set; }
        [Reactive] public DateTime Date { get; set; }

        private void UpdateValues(WeatherForecast forecast)
        {
            Date = forecast.Date;
            Temperature = forecast.TemperatureC;
            Windspeed = forecast.Windspeed;
            Humidity = forecast.Humidity;
        }

        private void UpdateWeather()
        {
            IsBusy = true;
            _weatherService.GetWeatherForecast(_location.Postalcode)
                .ObserveOn(RxApp.MainThreadScheduler)
                .Subscribe(
                    UpdateValues, 
                    HandleError, 
                    () => IsBusy = false);
        }
        
        private void HandleError(Exception o)
        {
            Debug.WriteLine(o);
        }
    }
}