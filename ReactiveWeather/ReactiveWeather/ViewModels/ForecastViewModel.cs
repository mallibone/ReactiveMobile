using System;
using System.Diagnostics;
using System.Reactive.Linq;
using System.Windows.Input;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using ReactiveWeather.Services;
using ReactiveWeather.ViewModels;
using WebApplication;
using DateTime = System.DateTime;
using Exception = System.Exception;

namespace ReactiveWeather.Views
{
    public class ForecastViewModel : ReactiveObject
    {
        private readonly WeatherService _weatherService;

        public ForecastViewModel(LocationViewItem location)
        {
            Location = location;
            _weatherService = new WeatherService();
            UpdateWeather();
            ExecuteUpdate = ReactiveCommand.Create(UpdateWeather);
        }

        public LocationViewItem Location { get; }
        [Reactive] public bool IsBusy { get; set; }

        [Reactive] public string CityName { get; set; }
        [Reactive] public int Humidity { get; set; }
        [Reactive] public int Windspeed { get; set; }
        [Reactive] public float Temperature { get; set; }
        [Reactive] public DateTime Date { get; set; }

        public ICommand ExecuteUpdate { get; set; }

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
            // _weatherService.GetWeatherForecast(Location.Postalcode)
            _weatherService.GetWeatherForecastCache(Location.Postalcode)
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