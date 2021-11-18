using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using Rx101.Helpers;
using Rx101.Helpers.Models;

namespace Rx101
{
    public static class Demo03
    {
        
        public static void MergeFlows()
        {
            Console.WriteLine("Merge Flows");
            // Testdata
            var sampleTemperatrues = new List<float> {12.5f, 43.2f, 22.3f, 21, 24, 27.8f, 21.3f, 33.2f};
            var sampleHumidity = new List<float> {40.1f,39,48.3f, 42.2f};
            
            var temperatureObserver = new ObservableSample();
            var humidityObserver = new ObservableSample();
            
            // Register Observer
            var subscription =
                temperatureObserver
                    .MeasurementChanged
                    .CombineLatest(humidityObserver.MeasurementChanged)
                    .Subscribe(measurementUpdate =>
                    {
                        var temperatureUpdate = measurementUpdate.First;
                        var humidityUpdate = measurementUpdate.Second;
                        
                        Console.WriteLine(
                            $"Current temperature: {temperatureUpdate.CurrentMeasurement}° C - Current humidity: {humidityUpdate.CurrentMeasurement}%");
                    });

            // Simulate Measurements
            for (int i = 0; i < 7; ++i)
            {
                temperatureObserver.NewMeasurementReading(sampleTemperatrues[i]);
                if (i % 2 == 0) humidityObserver.NewMeasurementReading(sampleHumidity[i / 2]);
            }
            
            // Clean Up
            subscription.Dispose();
        }
    }
}