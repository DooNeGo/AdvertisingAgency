using AdvertisingAgency.Application.Interfaces;
using AdvertisingAgency.Views;

namespace AdvertisingAgency;

public sealed partial class App
{
    private readonly IServiceProvider _serviceProvider;

    public App(IIdentityService identityService, IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
        InitializeComponent();

        MainPage = new NavigationPage(serviceProvider.GetRequiredService<LoginView>());
        UserAppTheme = AppTheme.Light;

        identityService.LoggedIn += _ => SetAppShellAsMain();
        identityService.LoggedOut += SetLoginPageAsMain;
    }

    public void SetLoginPageAsMain() =>
        MainPage = new NavigationPage(_serviceProvider.GetRequiredService<LoginView>());

    public void SetAppShellAsMain() => MainPage = new AppShell();
}