using System;
using System.Reactive;
using System.Reactive.Linq;
using System.Windows.Input;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace ReactiveWeather.ViewModels
{
    public class ErrorHandlingViewModel : ReactiveObject
    {
        public ErrorHandlingViewModel()
        {
            ExecuteReset = ReactiveCommand.CreateFromObservable<Unit, string>(_ => Observable.Return(String.Empty));
            ExecuteErrorRequest = ReactiveCommand.CreateFromObservable(ErrorRequest);
            
            // Error handling
            ExecuteErrorRequest
                .ThrownExceptions
                .Select(ex => ex.ToString())
                .Merge(ExecuteReset)
                .ToPropertyEx(this, vm => vm.ErrorText);

            this.WhenAnyValue(vm => vm.ErrorText)
                .Select(text => !string.IsNullOrWhiteSpace(text))
                .ToPropertyEx(this, vm => vm.HasError);
        }

        [ObservableAsProperty] public string ErrorText { get; set; }
        public ReactiveCommand<Unit, Unit> ExecuteErrorRequest { get; }
        public ReactiveCommand<Unit, string> ExecuteReset { get; }

        [ObservableAsProperty] public bool HasError { get; }

        private IObservable<Unit> ErrorRequest()
        {
            throw new NotImplementedException();
        }
    }
}