using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReactiveWeather.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ReactiveWeather.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ForecastPage : ContentPage
    {
        public ForecastPage(SevenDayForecast forecast)
        {
            InitializeComponent();
            var viewModel = new WeatherForecastViewModel(forecast);
            BindingContext = viewModel;
        }
    }
}