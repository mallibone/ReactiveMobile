using System;
using System.Reactive.Subjects;

namespace Rx101
{
    public class EventSample
    {
        public event EventHandler<MeasurementUpdate> MeasurementChanged;

        public void NewMeasruementReading(float measurement)
        {
            var measurementUpdate = new MeasurementUpdate(measurement);
            var measurementChanged = MeasurementChanged;
            if (measurementChanged == null) return;
            measurementChanged(this, measurementUpdate);
        }
    }

    public class ObservableSample
    {
        private readonly Subject<MeasurementUpdate> _measurementUpdateSubject = new();
        public IObservable<MeasurementUpdate> MeasurementChanged => _measurementUpdateSubject;

        public void NewMeasurementReading(float temperature) =>
            _measurementUpdateSubject.OnNext(new MeasurementUpdate(temperature));
    }

    public class MeasurementUpdate
    {
        public MeasurementUpdate(float currentMeasurement)
        {
            CurrentMeasurement = currentMeasurement;
        }
        public float CurrentMeasurement { get; }
    }
}