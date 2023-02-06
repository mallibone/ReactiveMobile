using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Windows.Input;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace Maui.Rx101.ViewModels;

public class MainViewModel : ReactiveObject, IDisposable
{
    private readonly CompositeDisposable _disposables;

    public MainViewModel()
    {
        _disposables = new CompositeDisposable();

        ExecuteCount = ReactiveCommand.Create(CountUp);

        this.WhenAnyValue(vm => vm.StepCount)
            .Select(c => c.ToString())
            .ToPropertyEx(this, vm => vm.CurrentStepCount)
            .DisposeWith(_disposables);
    }

    [ObservableAsProperty] public string CurrentStepCount { get; }
    [Reactive] public int Count { get; set; }

    [Reactive] public int StepCount { get; set; }

    [Reactive] public string CounterText { get; set; } = "Click me!";

    public ICommand ExecuteCount { get; set; }

    private void CountUp()
    {
        Count += StepCount;
        CounterText = $"Count is at {Count}";
    }

    public void Dispose()
    {
        _disposables.Dispose();
    }
}

