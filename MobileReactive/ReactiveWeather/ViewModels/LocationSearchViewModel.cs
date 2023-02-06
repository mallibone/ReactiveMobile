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
        private readonly LocalityService _localityService;

        public LocationSearchViewModel()
        {
            _localityService = new LocalityService();
            // The Commands
            ExecuteSearch =
                ReactiveCommand.CreateFromObservable<string, IEnumerable<LocationViewItem>>(searchEntry =>
                    Search(searchEntry));
            ExecuteSearch.ThrownExceptions.Subscribe(ex => HandleException(ex));

            // The Search
            #region search
            // this.WhenAnyValue(vm => vm.SearchEntry)
            //     // .ObserveOn(RxApp.TaskpoolScheduler) // for heavy lifting change to background thread
            //     .Throttle(TimeSpan.FromMilliseconds(300))
            //     .Select(query => query?.Trim())
            //     .Where(query => query != null)
            //     .DistinctUntilChanged()
            //     .ObserveOn(RxApp.MainThreadScheduler) // change to UI thread
            //     .InvokeCommand(ExecuteSearch);
            #endregion

            // The Navigation
            #region navigation
            // this.WhenAnyValue(vm => vm.SelectedLocation)
            //     .Where(sl => sl != null)
            //     .Do(_ => SelectedLocation = null)
            //     .Subscribe(sl => NavigateToForecast(sl));
            #endregion
        }

        private IObservable<IEnumerable<LocationViewItem>> Search(string searchEntry)
        {
            IsBusy = true;
            return
                    _localityService.SearchLocalities(searchEntry)
                    .Where(localities => localities != null)
                    .Select(localities =>
                        localities.Select(l => new LocationViewItem {City = l.City, Postalcode = l.Postalcode})
                            .ToList())
                    .ObserveOn(RxApp.MainThreadScheduler)
                    .Do(l => Locations = l)
                    .Do(_ => IsBusy = false);
        }

        public Func<LocationViewItem, Task> NavigateToForecast { get; set; } = _ => Task.CompletedTask;
        [Reactive] public bool IsBusy { get; set; }
        [Reactive] public string SearchEntry { get; set; } = string.Empty;
        [Reactive] public IEnumerable<LocationViewItem> Locations { get; set; } = Enumerable.Empty<LocationViewItem>();
        [Reactive] public LocationViewItem SelectedLocation { get; set; } = null!;
        public ReactiveCommand<string,IEnumerable<LocationViewItem>> ExecuteSearch { get; }

        private void HandleException(Exception exception)
        {
            Console.WriteLine(exception);
            // todo: add error handling and logging
        }
    }
}