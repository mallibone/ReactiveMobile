using System;

namespace Rx101.Helpers
{
    public class EventSample
    {
        public event EventHandler<MeasurementUpdate> MeasurementChanged;

        public void NewMeasruementReading(float measurement)
        {
            var measurementUpdate = new MeasurementUpdate(measurement);
            var measurementChanged = MeasurementChanged;
            if (measurementChanged == null) return;
            measurementChanged(this, measurementUpdate);
        }
    }
}