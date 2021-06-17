namespace Rx101.Helpers
{
    public class MeasurementUpdate
    {
        public MeasurementUpdate(float currentMeasurement)
        {
            CurrentMeasurement = currentMeasurement;
        }
        public float CurrentMeasurement { get; }
    }
}