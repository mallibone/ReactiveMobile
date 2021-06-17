using System;
using System.Reactive.Subjects;

namespace Rx101.Helpers
{
    public class ObservableSample
    {
        private readonly Subject<MeasurementUpdate> _measurementUpdateSubject = new();
        public IObservable<MeasurementUpdate> MeasurementChanged => _measurementUpdateSubject;

        public void NewMeasurementReading(float temperature) =>
            _measurementUpdateSubject.OnNext(new MeasurementUpdate(temperature));
    }
}