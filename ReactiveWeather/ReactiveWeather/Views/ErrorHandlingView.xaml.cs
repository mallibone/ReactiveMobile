using ReactiveWeather.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ReactiveWeather.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ErrorHandlingView : ContentView
    {
        public ErrorHandlingView()
        {
            InitializeComponent();
            BindingContext = new ErrorHandlingViewModel();
        }
    }
}