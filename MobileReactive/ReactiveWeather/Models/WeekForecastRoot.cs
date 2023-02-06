namespace ReactiveWeather.Models;

public class WeekForecastRoot
{
    public SevenDayForecast Data { get; set; } = new ();
    public Config Config { get; set; } = new();
}