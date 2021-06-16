using System.Threading.Tasks;
using ReactiveWeather.Models;
using ReactiveWeather.ViewModels;
using Xamarin.Forms;

namespace ReactiveWeather.Views
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            var viewModel = new WeatherPortalViewModel {NavigateToForecast = NavigateToForecast};
            BindingContext = viewModel;
        }

        private Task NavigateToForecast(SevenDayForecast forecast)
        {
            return Navigation.PushAsync(new ForecastPage(forecast));
        }
    }
}
