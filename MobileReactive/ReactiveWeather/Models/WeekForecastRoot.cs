namespace ReactiveWeather.Models
{
    public class WeekForecastRoot
    {
        public SevenDayForecast Data { get; set; }
        public Config Config { get; set; }
    }
}