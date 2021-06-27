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
    public partial class TimeView : ContentView
    {
        public TimeView()
        {
            InitializeComponent();
            BindingContext = new TimeViewModel();
        }
    }
}