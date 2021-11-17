using System;
using Rx101.Helpers;
using Rx101.Helpers.Models;

namespace Rx101
{
    public static class Demo01
    {
        public static void SimpleComparison()
        {
            Console.WriteLine("Simple Event comparison");
            RunEventSample();
            RunObservableSample();
        }

        private static void RunObservableSample()
        {
            var observableSample = new ObservableSample();
            
            var measurementSubscription =
                observableSample.MeasurementChanged.Subscribe(
                    update => Console.WriteLine($"Temperature update: {update.CurrentMeasurement}"),
                    exception => Console.WriteLine($"Hoppla: {exception}"),
                    () => Console.WriteLine("All done."));
            
            observableSample.NewMeasurementReading(24.0f);
            measurementSubscription.Dispose();
        }

        private static void RunEventSample()
        {
            var eventSample = new EventSample();
            eventSample.MeasurementChanged += EventSampleOnMeasurementChanged;
            eventSample.NewMeasruementReading(22.0f);
            eventSample.MeasurementChanged -= EventSampleOnMeasurementChanged;
        }
        private static void EventSampleOnMeasurementChanged(object sender, MeasurementUpdate update) =>
            Console.WriteLine($"Temperature update {update.CurrentMeasurement}");
    }
}