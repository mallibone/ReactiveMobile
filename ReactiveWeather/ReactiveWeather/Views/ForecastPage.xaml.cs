using ReactiveUI.XamForms;
using ReactiveWeather.ViewModels;
using Xamarin.Forms.Xaml;

namespace ReactiveWeather.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ForecastPage : ReactiveContentPage<WeatherForecastViewModel>
    {
        public ForecastPage(LocationViewItem location)
        {
            InitializeComponent();
            ViewModel = new WeatherForecastViewModel(location);
            BindingContext = ViewModel;
        }
    }
}