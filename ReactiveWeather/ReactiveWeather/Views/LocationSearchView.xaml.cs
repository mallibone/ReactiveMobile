using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReactiveWeather.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ReactiveWeather.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LocationSearchView : ContentView
    {
        public LocationSearchView()
        {
            InitializeComponent();
            var viewModel = new LocationSearchViewModel();
            BindingContext = viewModel;
        }
    }
}