using System;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using DynamicData.Binding;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using ReactiveWeather.Models;
using ReactiveWeather.Services;

namespace ReactiveWeather.ViewModels
{
    public class WeatherPortalViewModel : ReactiveObject
    {
        private readonly WeatherService _weatherService;

        public WeatherPortalViewModel()
        {
            _weatherService = new WeatherService();
            ExecuteSearch = ReactiveCommand.CreateFromObservable(Search);
        }

        private IObservable<Unit> Search()
        {
            return
                _weatherService
                    .GetSevenDayForecast(Searchentry)
                    .Do(AddToLocations)
                    .SelectMany(forecast => Observable.StartAsync(() => NavigateToForecast(forecast)));
        }

        private void AddToLocations(SevenDayForecast forecast)
        {
            if (Locations.Any(l => l.City == forecast.CityName)) return;
            Locations.Add(new Location{City = forecast.CityName, LocationId = forecast.LocationId});
        }


        [Reactive] public int Searchentry { get; set; }
        public Func<SevenDayForecast, Task> NavigateToForecast { get; set; } = forecast => Task.CompletedTask;

        public ICommand ExecuteSearch { get; }

        public IObservableCollection<Location> Locations { get; set; } = new ObservableCollectionExtended<Location>();
    }

    public class Location
    {
        public string City { get; set; }
        public string LocationId { get; set; }
        public int Postalcode { get; set; }
    }
}