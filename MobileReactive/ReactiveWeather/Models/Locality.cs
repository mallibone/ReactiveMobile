namespace ReactiveWeather.Models
{
    public class Locality
    {
        public int Postalcode { get; set; }
        public string City { get; set; } = string.Empty;
        public string Canton { get; set; } = string.Empty;
        public string Abbreviation { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
    }
}