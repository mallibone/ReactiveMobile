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

        private static void RunObservableSample()
        {
            // var observableSample = new ObservableSample();
            var observableSample = new ObservableStreamSample();
            // var observableSample = new ObservableStreamSample(true);
            
            Console.WriteLine("Observable");
            Console.WriteLine("****************");
            var measurementSubscription =
                observableSample.MeasurementChanged.Subscribe(
                    // update => Console.WriteLine($"Temperature update: {update.CurrentMeasurement}"));
                    update => Console.WriteLine($"Temperature update: {update.CurrentMeasurement}"),
                    // exception => Console.WriteLine($"Hoppla: {exception.Message}"),
                    () => Console.WriteLine("All done."));
            
            // observableSample.NewMeasurementReading(24.0f);
            // observableSample.NewMeasurementReading(13.37f, true);
            measurementSubscription.Dispose();
        }
    }
}