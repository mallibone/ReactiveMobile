namespace ReactiveWeather;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();

		MainPage = new AppShell();
	}

    protected override void OnStart()
    {
        base.OnStart();
        
        Akavache.Registrations.Start(AppInfo.Name);
    }
}

