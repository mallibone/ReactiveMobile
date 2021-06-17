using System;
using Rx101.Helpers;

namespace Rx101
{
    public static class Demo01
    {
        public static void SimpleComparison()
        {
            Console.WriteLine("Simple Event comparison");
            var eventSample = new EventSample();
            eventSample.MeasurementChanged += (_, update) => Console.WriteLine($"Temperature update {update.CurrentMeasurement}");

            var observableSample = new ObservableSample();
            observableSample.MeasurementChanged.Subscribe(update =>
                Console.WriteLine($"Temperature update {update.CurrentMeasurement}"));
            
            eventSample.NewMeasruementReading(22.0f);
            observableSample.NewMeasurementReading(24.0f);
        }
    }
}