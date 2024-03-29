using System;
using System.Diagnostics;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Windows.Input;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using ReactiveWeather.Models;
using ReactiveWeather.Services;
using DateTime = System.DateTime;
using Exception = System.Exception;

namespace ReactiveWeather.ViewModels;

public class ForecastViewModel : ReactiveObject, IActivatableViewModel
{
    private readonly WeatherService _weatherService;

    public ForecastViewModel(LocationViewItem location)
    {
        Location = location;
        _weatherService = new WeatherService();
        // UpdateWeather();
        ExecuteUpdate = ReactiveCommand.CreateFromObservable(UpdateWeather);
        this.WhenActivated(disposable =>
        {
            UpdateWeather(disposable);
        });
    }

    public LocationViewItem Location { get; }
    [Reactive] public bool IsBusy { get; set; }

    [Reactive] public string CityName { get; set; } = string.Empty;
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

    private IObservable<WeatherForecast> UpdateWeather()
    {
        // _weatherService.GetWeatherForecast(Location.Postalcode)
        return _weatherService.GetWeatherForecastCache(Location.Postalcode)
            .ObserveOn(RxApp.MainThreadScheduler)
            .Do(UpdateValues);
    }

    private void UpdateWeather(CompositeDisposable compositeDisposable)
    {
        IsBusy = true;
        // _weatherService.GetWeatherForecast(Location.Postalcode)
        UpdateWeather()
            .Subscribe(
                UpdateValues, 
                HandleError, 
                () => IsBusy = false)
            .DisposeWith(compositeDisposable);
    }
    
    private void HandleError(Exception o)
    {
        Debug.WriteLine(o);
    }

    public ViewModelActivator Activator { get; } = new();
}