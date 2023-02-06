using System;
using System.Reactive.Linq;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace ReactiveWeather.ViewModels;

public class TimeViewModel : ReactiveObject
{
    public TimeViewModel()
    {
        CurrentTime = DateTime.Now.ToString("T");
        
        Observable.Interval(TimeSpan.FromSeconds(1), RxApp.MainThreadScheduler)
            .Select(_ => DateTime.Now.ToString("T"))
            .ToPropertyEx(this, vm => vm.CurrentTime);
    }
    [ObservableAsProperty] public string CurrentTime { get; }
}