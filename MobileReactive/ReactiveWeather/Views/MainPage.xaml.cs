using Microsoft.Maui.Controls;
using ReactiveWeather.ViewModels;
using ReactiveWeather.Views;

namespace ReactiveWeather;

public partial class MainPage : ContentPage
{
	public MainPage()
	{
		InitializeComponent();
		var viewModel = new LocationSearchViewModel
		{
			NavigateToForecast = NavigateToForecast
		};
		BindingContext = viewModel;
	}
	
	private Task NavigateToForecast(LocationViewItem location) => Navigation.PushAsync(new ForecastPage(location));
}


