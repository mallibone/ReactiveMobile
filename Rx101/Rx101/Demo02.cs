using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using Rx101.Helpers;

namespace Rx101
{
    public static class Demo02
    {
        public static void FilterEventFlows()
        {
            Console.WriteLine("Simple Event comparison");
            var sampleData = new List<float> {12.5f, 43.2f, 22.3f, 21, 24, 27.8f, 21.3f, 33.2f};
            const float maxTemperature = 23.0f;
            var eventSample = new EventSample();
            eventSample.MeasurementChanged += (_, update) =>
            {
                if (update.CurrentMeasurement > maxTemperature) HandleTemperatureUpdate(update);
            };

            var observableSample = new ObservableSample();
            observableSample
                .MeasurementChanged
                .Where(update => update.CurrentMeasurement > maxTemperature)
                .Subscribe(HandleTemperatureUpdate);
            foreach (var dataPoint in sampleData)
            {
                eventSample.NewMeasruementReading(dataPoint);
                observableSample.NewMeasurementReading(dataPoint);
            }
        }

        private static void HandleTemperatureUpdate(MeasurementUpdate update) =>
            Console.WriteLine($"Temperature update {update.CurrentMeasurement}");
    }
}