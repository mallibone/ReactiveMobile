using System;
using System.Diagnostics;
using System.Reactive.Subjects;
using Xunit;

namespace Rx101.Test
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            void HandleTemperatureUpdate(TemperatureUpdate update) =>
                Debug.WriteLine($"Temperature update {update.CurrentTemperature}");
            
            var eventSample = new EventSample();
            eventSample.TemperatureChanged += (_, update) => HandleTemperatureUpdate(update);

            var observableSample = new ObservableSample();
            observableSample.TemperatureChanged.Subscribe(HandleTemperatureUpdate);
            
            eventSample.NewTemperatureReading(22.0f);
            observableSample.NewTemperatureReading(24.0f);
        }

    }
    
    public class EventSample
    {
        public event EventHandler<TemperatureUpdate> TemperatureChanged;

        public void NewTemperatureReading(float temperature)
        {
            var temperatureUpdate = new TemperatureUpdate(temperature);
            var temperatureChanged = TemperatureChanged;
            if (temperatureChanged == null) return;
            temperatureChanged(this, temperatureUpdate);
        }
    }

    public class ObservableSample
    {
        private readonly Subject<TemperatureUpdate> _temperatureUpdateSubject = new();
        public IObservable<TemperatureUpdate> TemperatureChanged => _temperatureUpdateSubject;

        public void NewTemperatureReading(float temperature) =>
            _temperatureUpdateSubject.OnNext(new TemperatureUpdate(temperature));
    }

    public class TemperatureUpdate
    {
        public TemperatureUpdate(float currentTemperature)
        {
            CurrentTemperature = currentTemperature;
        }
        public float CurrentTemperature { get; }
    }
}