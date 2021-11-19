using System;
using System.Linq;
using System.Reactive.Linq;
using Rx101.Helpers.Models;

namespace Rx101.Helpers
{
    public class EventStreamSample
    {
        private readonly Random _random = new();
        public event EventHandler<MeasurementUpdate> MeasurementChanged;

        public void GenerateMeasurementReadings()
        {
            var measurements = Enumerable.Range(0, 5).Select(_ => new MeasurementUpdate(_random.Next(190, 283)/10f));
            foreach(var measurement in measurements)
            {
                var measurementChanged = MeasurementChanged;
                if (measurementChanged is null) return;
                measurementChanged(this, measurement);
            }
        }
    }

    public class ObservableStreamSample
    {
        private readonly Random _random = new();
        private readonly bool _goBoom;

        public ObservableStreamSample(bool goBoom = false)
        {
            _goBoom = goBoom;
        }

        public IObservable<MeasurementUpdate> MeasurementChanged => 
            Enumerable.Range(0, 5)
            .Select(_ => new MeasurementUpdate(_random.Next(190, 283) / 10f))
            .ToObservable();
            // .Select(i =>
            //     i == 4 && _goBoom
            //         ? throw new Exception("We went boom... 💥")
            //         : new MeasurementUpdate(_random.Next(190, 283) / 10f))
    }
}