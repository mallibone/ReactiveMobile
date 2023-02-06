using System;
using System.Reactive.Subjects;
using Rx101.Helpers.Models;

namespace Rx101.Helpers;

public class EventVsObservableSample
{
    #region Rx
    private readonly Subject<MeasurementUpdate> _measurementSubject = new();
    public IObservable<MeasurementUpdate> MeasurementChangedObservable => _measurementSubject;
    #endregion

    #region Event
    public event EventHandler<MeasurementUpdate> MeasurementChanged;
    #endregion

    public void NewMeasruementReading(float measurement)
    {
        var measurementUpdate = new MeasurementUpdate(measurement);

        // Rasing the event
        var measurementChanged = MeasurementChanged;
        if (measurementChanged is null) return;
        measurementChanged(this, measurementUpdate);
        
        // Pushing the value to the observable
        _measurementSubject.OnNext(measurementUpdate);
    }
}
