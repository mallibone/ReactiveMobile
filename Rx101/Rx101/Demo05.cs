using System;
using System.Reactive.Concurrency;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using Rx101.Helpers;

namespace Rx101
{
    public class Demo05 : IDisposable
    {
        private readonly CompositeDisposable _compositeDisposable = new();

        public Demo05(TimeSpan interval = default, IScheduler scheduler = null)
        {
            Console.WriteLine("Timer");
            // Setup
            var timerScheduler = scheduler ?? Scheduler.Default;
            var timerInterval = interval == default ? TimeSpan.FromSeconds(1) : interval;
            
            var observer = new ObservableSample();
            var random = new Random(43);

            // Implement Timer
            var timerSubscription = Observable
                .Interval(timerInterval, timerScheduler)
                .Subscribe(x => observer.NewMeasurementReading(random.Next(200, 300) / 10f));
            _compositeDisposable.Add(timerSubscription);
            
            // Observe measurements
            _compositeDisposable.Add(
                observer.MeasurementChanged.Subscribe(m =>
                    Console.WriteLine($"New temperature: {m.CurrentMeasurement}° C")));
        }

        public void Dispose()
        {
            _compositeDisposable.Dispose();
        }
    }
}