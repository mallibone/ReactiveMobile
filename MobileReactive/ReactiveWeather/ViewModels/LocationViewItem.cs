using ReactiveUI.Fody.Helpers;

namespace ReactiveWeather.ViewModels;

public class LocationViewItem
{
    [Reactive] public string City { get; set; } = string.Empty;
    [Reactive] public int Postalcode { get; set; }
    public override string ToString()
    {
        return $"{Postalcode} {City}";
    }
}