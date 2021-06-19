using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reactive.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ReactiveUI;
using ReactiveWeather.Models;

namespace ReactiveWeather.Services
{
    public class LocalityService
    {
        private List<Locality> _localities = new();

        public IObservable<IEnumerable<Locality>> SearchLocalities(string searchQuery) =>
            Observable
                .StartAsync(() => Filter(searchQuery), RxApp.TaskpoolScheduler)
                .ObserveOn(RxApp.MainThreadScheduler);

        private static int counter = 0;
        private async Task<IEnumerable<Locality>> Filter(string searchQuery)
        {
            if (_localities.Any() == false) _localities = await LoadPostalcodes();
            
            // Adds a random break on every odd search request
            // if (counter % 2 == 1) await Task.Delay(TimeSpan.FromSeconds(5));

            return _localities.Where(l => l.City.StartsWith(searchQuery) || l.Postalcode.ToString().StartsWith(searchQuery));
        }
        
        private async Task<List<Locality>> LoadPostalcodes()
        {
            var assembly = IntrospectionExtensions.GetTypeInfo(typeof(App)).Assembly;
            Stream stream = assembly.GetManifestResourceStream("ReactiveWeather.Assets.SwissPostalcodes.json");
            using StreamReader reader = new System.IO.StreamReader(stream);
            string postalCodeJson = await reader.ReadToEndAsync();
            return JsonConvert.DeserializeObject<List<Locality>>(postalCodeJson);
        }
    }
}