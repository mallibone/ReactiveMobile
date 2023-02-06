using System.Reactive.Linq;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace Maui.Rx101.ViewModels;

public class TimerViewModel:ReactiveObject
{
    public TimerViewModel()
    {
        Observable.Interval(TimeSpan.FromSeconds(1), RxApp.MainThreadScheduler)
            .Subscribe(_ => Time = DateTime.Now.ToString("hh:mm:ss"));
    }

    [Reactive] public string Time { get; set; } = DateTime.Now.ToString("hh:mm:ss");
}