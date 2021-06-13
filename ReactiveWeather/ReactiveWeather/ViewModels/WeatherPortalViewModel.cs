using System.Collections.Generic;
using System.Reactive.Linq;
using ReactiveUI;

namespace ReactiveWeather.ViewModels
{
    public class WeatherPortalViewModel : ReactiveObject
    {
        public WeatherPortalViewModel()
        {
            var gna = new List<int> {1, 2, 3, 4, 5}.ToObservable();
        }
    }
    
    
}