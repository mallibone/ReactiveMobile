using System;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using Rx101.Helpers.Models;

namespace Rx101.Helpers
{
    public class ObservableSample
    {
        // Subject
        private readonly Subject<MeasurementUpdate> _measurementUpdateSubject = new();
        // Observable
        public IObservable<MeasurementUpdate> MeasurementChanged => _measurementUpdateSubject.AsObservable();

        public void NewMeasurementReading(float measurement, bool goBoom = false)
        {
            if(goBoom) _measurementUpdateSubject.OnError(new Exception("We went boom... 💥"));
            _measurementUpdateSubject.OnNext(new MeasurementUpdate(measurement));
        }
    }
}