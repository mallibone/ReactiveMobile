using System.Reactive.Linq;
using System.Reflection;
using System.Text.Json;
using ReactiveUI;
using ReactiveWeather.Models;

namespace ReactiveWeather.Services;

public class LocalityService
{
    private List<Locality> _localities = new();

    public IObservable<IEnumerable<Locality>> SearchLocalities(string searchQuery) =>
        Observable
            .StartAsync((tcl) => Filter(searchQuery, tcl), RxApp.TaskpoolScheduler)
            .ObserveOn(RxApp.MainThreadScheduler);

    private async Task<IEnumerable<Locality>> Filter(string searchQuery, CancellationToken tcl)
    {
        if (_localities.Any() == false) _localities = await LoadPostalcodes();
        
        // Adds a random break on every search request
        await Task.Delay(TimeSpan.FromMilliseconds(1000), tcl);

        return string.IsNullOrEmpty(searchQuery)
            ? new List<Locality>()
            : _localities.Where(l =>
                l.City.StartsWith(searchQuery, StringComparison.InvariantCultureIgnoreCase)
                || l.Postalcode.ToString().StartsWith(searchQuery));
    }
    
    private async Task<List<Locality>> LoadPostalcodes()
    {
        Assembly assembly = IntrospectionExtensions.GetTypeInfo(typeof(App)).Assembly;
        Stream? stream = assembly.GetManifestResourceStream("ReactiveWeather.Assets.SwissPostalcodes.json");
        using StreamReader reader = new StreamReader(stream!);
        string postalCodeJson = await reader.ReadToEndAsync();
        return JsonSerializer.Deserialize<List<Locality>>(postalCodeJson)!;
    }
}