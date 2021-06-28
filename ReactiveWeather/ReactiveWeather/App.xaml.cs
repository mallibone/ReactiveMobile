using System;
using ReactiveWeather.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ReactiveWeather
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            Akavache.Registrations.Start("ReactiveWeather");

            MainPage = new NavigationPage(new MainPage());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
