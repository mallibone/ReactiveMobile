using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace Rx101
{
    class Program
    {
        static void Main(string[] _)
        {
            // SimpleComparison();
            // FilterEventFlows();
            MergeFlows();
            Console.ReadLine();
        }

        private static void MergeFlows()
        {
            var sampleTemperatrues = new List<float> {12.5f, 43.2f, 22.3f, 21, 24, 27.8f, 21.3f, 33.2f};
            var sampleHumidity = new List<float> {40.1f,39,48.3f, 42.2f};
            
            var temperatureObserver = new ObservableSample();
            var humidityObserver = new ObservableSample();
            temperatureObserver
                .MeasurementChanged
                .CombineLatest(humidityObserver.MeasurementChanged)
                .Subscribe(measurementUpdate =>
                {
                    MeasurementUpdate temperatureUpdate = measurementUpdate.First;
                    MeasurementUpdate humidityUpdate= measurementUpdate.Second;
                    
                    Console.WriteLine(
                        $"Current temperature: {temperatureUpdate.CurrentMeasurement}° C - Current humidity: {humidityUpdate.CurrentMeasurement}%");
                });

            for (int i = 0; i < 7; ++i)
            {
                temperatureObserver.NewMeasurementReading(sampleTemperatrues[i]);
                if(i%2==0) humidityObserver.NewMeasurementReading(sampleHumidity[i/2]);
            }
        }

        private static void FilterEventFlows()
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

        private static void SimpleComparison()
        {
            Console.WriteLine("Simple Event comparison");
            var eventSample = new EventSample();
            eventSample.MeasurementChanged += (_, update) => HandleTemperatureUpdate(update);

            var observableSample = new ObservableSample();
            observableSample.MeasurementChanged.Subscribe(HandleTemperatureUpdate);
            
            eventSample.NewMeasruementReading(22.0f);
            observableSample.NewMeasurementReading(24.0f);
        }

        private static void HandleTemperatureUpdate(MeasurementUpdate update) =>
            Console.WriteLine($"Temperature update {update.CurrentMeasurement}");
    }

}