using System;
using Rx101.Helpers;
using Rx101.Helpers.Models;

namespace Rx101;

public static class Demo01
{
    public static void SimpleComparison()
    {
        Console.WriteLine("Simple Event comparison");
        RunEventSample();
        RunObservableSample();
    }

    #region Event Sample
    private static void RunEventSample()
    {
        Console.WriteLine("Event");
        Console.WriteLine("****************");

        var eventSample = new EventStreamSample();
        eventSample.MeasurementChanged += EventSampleOnMeasurementChanged;
        eventSample.GenerateMeasurementReadings();
        eventSample.MeasurementChanged -= EventSampleOnMeasurementChanged;
    }
    private static void EventSampleOnMeasurementChanged(object sender, MeasurementUpdate update) =>
        Console.WriteLine($"Temperature update {update.CurrentMeasurement}");
    #endregion

    #region Observable
    private static void RunObservableSample()
    {
        var observableSample = new ObservableStreamSample(true);
            
        Console.WriteLine("Observable");
        Console.WriteLine("****************");

        // +=
        var measurementSubscription =
            observableSample.MeasurementChanged.Subscribe(
                update => Console.WriteLine($"Temperature update: {update.CurrentMeasurement}"),
                exception => Console.WriteLine($"Ooops: {exception.Message}"),
                () => Console.WriteLine("All done."));

        // -=
        measurementSubscription.Dispose();
    }
    #endregion
}