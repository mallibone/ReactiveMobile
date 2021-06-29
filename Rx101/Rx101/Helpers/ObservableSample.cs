using System;
using System.Reactive.Subjects;
using Rx101.Helpers.Models;

namespace Rx101.Helpers
{
    public class ObservableSample
    {
        // Subject
        private readonly Subject<MeasurementUpdate> _measurementUpdateSubject = new();
        // Observable
        public IObservable<MeasurementUpdate> MeasurementChanged => _measurementUpdateSubject;

        public void NewMeasurementReading(float measurement) =>
            _measurementUpdateSubject.OnNext(new MeasurementUpdate(measurement));
    }
}