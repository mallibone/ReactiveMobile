using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReactiveUI;
using ReactiveUI.Maui;
using ReactiveWeather.ViewModels;

namespace ReactiveWeather.Views;

public partial class ForecastPage : ReactiveContentPage<ForecastViewModel>
{
    public ForecastPage(LocationViewItem location)
    {
        InitializeComponent();
        ViewModel = new ForecastViewModel(location);
        BindingContext = ViewModel;
        this.WhenActivated(_ => { });
    }
}