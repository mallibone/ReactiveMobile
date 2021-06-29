using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using Rx101.Helpers;
using Rx101.Helpers.Models;

namespace Rx101
{
    public static class Demo02
    {
        private const float temperatureThreshhold = 23.0f;
        public static void FilterEventFlows()
        {
            Console.WriteLine("Filter Event Flows");
            // Setup
            var sampleData = new List<float> {12.5f, 43.2f, 22.3f, 21, 24, 27.8f, 21.3f, 33.2f};
            // Event
            var eventSample = new EventSample();
            eventSample.MeasurementChanged += OnEventSampleOnMeasurementChanged;

            // Observable
            var observableSample = new ObservableSample();
            var subscription =
                observableSample.MeasurementChanged
                    .Where(update => update.CurrentMeasurement > temperatureThreshhold)
                    .Subscribe(HandleTemperatureUpdate);
            
            
            // Send measurements
            foreach (var dataPoint in sampleData)
            {
                eventSample.NewMeasruementReading(dataPoint);
                observableSample.NewMeasurementReading(dataPoint);
            }
            
            // Clean Up
            subscription.Dispose();
            eventSample.MeasurementChanged -= OnEventSampleOnMeasurementChanged;
        }

        private static void OnEventSampleOnMeasurementChanged(object _, MeasurementUpdate update)
        {
            if (update.CurrentMeasurement > temperatureThreshhold) HandleTemperatureUpdate(update);
        }

        private static void HandleTemperatureUpdate(MeasurementUpdate update) =>
            Console.WriteLine($"Temperature update {update.CurrentMeasurement}");
    }
}