using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Windows.Input;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace Maui.Rx101.ViewModels;

public class MainViewModel : ReactiveObject, IDisposable
{
    private readonly CompositeDisposable _disposables = new CompositeDisposable();

    public MainViewModel()
    {
        ExecuteCount = ReactiveCommand.Create(CountUp);

        this.WhenAnyValue(vm => vm.StepCount)
            .Select(c => c.ToString())
            .ToPropertyEx(this, vm => vm.CurrentStepCount)
            .DisposeWith(_disposables);
    }

    [ObservableAsProperty] public string CurrentStepCount { get; } = string.Empty;
    [Reactive] public int Count { get; set; } = 0;

    [Reactive] public int StepCount { get; set; } = 1;

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

