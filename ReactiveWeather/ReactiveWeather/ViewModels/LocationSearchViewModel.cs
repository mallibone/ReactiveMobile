using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using ReactiveWeather.Models;
using ReactiveWeather.Services;

namespace ReactiveWeather.ViewModels
{
    public class LocationSearchViewModel : ReactiveObject
    {
        private readonly WeatherService _weatherService;
        private readonly LocalityService _locatityService;

        public LocationSearchViewModel()
        {
            _weatherService = new WeatherService();
            _locatityService = new LocalityService();
            ExecuteSearch =
                ReactiveCommand.CreateFromObservable<string, IEnumerable<LocationViewItem>>(searchEntry => Search(searchEntry));
            ExecuteSearch.ThrownExceptions.Subscribe(ex => HandleException(ex));

            this.WhenAnyValue(vm => vm.SearchEntry)
                .Throttle(TimeSpan.FromMilliseconds(500), RxApp.TaskpoolScheduler)
                .Select(query => query?.Trim())
                .Where(query => query != null)
                .DistinctUntilChanged()
                .ObserveOn(RxApp.MainThreadScheduler)
                .Select(query => ExecuteSearch.Execute(query))
                .Switch()
                .Subscribe();

            this.WhenAnyValue(vm => vm.SelectedLocation)
                .Where(sl => sl != null)
                .Do(_ => SelectedLocation = null)
                .Subscribe(sl => NavigateToForecast(sl));
        }

        private IObservable<IEnumerable<LocationViewItem>> Search(string searchEntry)
        {
            IsBusy = true;
            return
                    _locatityService.SearchLocalities(searchEntry)
                    .Where(localities => localities != null)
                    .Select(localities =>
                        localities.Select(l => new LocationViewItem {City = l.City, Postalcode = l.Postalcode})
                            .ToList())
                    .ObserveOn(RxApp.MainThreadScheduler)
                    .Do(l => Locations = l)
                    .Do(_ => IsBusy = false);
        }

        [Reactive] public bool IsBusy { get; set; }

        // private int _searchEntry;
        // public int SearchEntry
        // {
        //     get => _searchEntry; 
        //     set => this.RaiseAndSetIfChanged(ref _searchEntry, value);
        // }

        // identical to the written out above - generated at compile time
        [Reactive] public string SearchEntry { get; set; }
        public Func<LocationViewItem, Task> NavigateToForecast { get; set; } = location => Task.CompletedTask;

        // public ICommand ExecuteSearch { get; }
        public ReactiveCommand<string,IEnumerable<LocationViewItem>> ExecuteSearch { get; }

        [Reactive] public IEnumerable<LocationViewItem> Locations { get; set; }

        [Reactive] public LocationViewItem SelectedLocation { get; set; }

        private void HandleException(Exception exception)
        {
            throw new NotImplementedException();
        }
    }
}