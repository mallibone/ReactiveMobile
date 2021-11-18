using System;
using System.Collections.Generic;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace HelloReactiveUI
{
    public class MainViewModel : ReactiveObject
    {
        private int _delayInSeconds = 1;

        public MainViewModel()
        {
            ExecuteAsyncIncrement = ReactiveCommand.CreateFromTask(DelayedCount);
            // ExecuteAsyncIncrement = ReactiveCommand.CreateFromObservable(ObservableDelayedCount);
            // ExecuteIncrement = ReactiveCommand.Create(() => Counter++, ExecuteAsyncIncrement.CanExecute);
            ExecuteIncrement = ReactiveCommand.Create(() => Counter++);

            this.WhenAnyValue(vm => vm.Counter)
                .Do(counter =>
                {
                    if (counter > 0) ButtonText = "Keep on tapping!";
                })
                .Select(counter => $"Button has been clicked {counter} times.")
                .ToPropertyEx(this, vm => vm.CounterMessage);
        }

        [Reactive] public string ButtonText { get; set; } = "Tap me!";
        [Reactive] public int Counter { get; set; }

        public int DelayInSeconds
        {
            get => _delayInSeconds;
            set => this.RaiseAndSetIfChanged(ref _delayInSeconds, value);
        }
        
        public string CounterMessage { [ObservableAsProperty] get; }

        public ICommand ExecuteIncrement { get; }
        public ReactiveCommand<Unit, Unit> ExecuteAsyncIncrement { get; }

        private async Task DelayedCount()
        {
            await Task.Delay(TimeSpan.FromSeconds(DelayInSeconds));
            Counter++;
        }
        
        private IObservable<Unit> ObservableDelayedCount() =>
            Observable.Start(() => { /* Do Something */ })
                .Delay(TimeSpan.FromSeconds(DelayInSeconds))
                .Do(_ => Counter++);
    }
}