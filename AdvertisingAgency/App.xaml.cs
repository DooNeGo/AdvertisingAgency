using AdvertisingAgency.Application.Interfaces;
using AdvertisingAgency.Views;
using AsyncAwaitBestPractices;

namespace AdvertisingAgency;

public sealed partial class App
{
    private readonly IServiceProvider _serviceProvider;

    public App(IIdentityService identityService, IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
        InitializeComponent();

        DevExpress.Maui.Editors.Initializer.Init();

        MainPage = new NavigationPage(serviceProvider.GetRequiredService<LoginView>());
        UserAppTheme = AppTheme.Light;

        identityService.LoggedIn += _ => SetAppShellAsMain();
        identityService.LoggedOut += SetLoginPageAsMain;
    }

    public void SetLoginPageAsMain() => Dispatcher
        .DispatchAsync(() => MainPage = new NavigationPage(_serviceProvider.GetRequiredService<LoginView>()))
        .SafeFireAndForget();

    public void SetAppShellAsMain() => Dispatcher
        .DispatchAsync(() => MainPage = new AppShell())
        .SafeFireAndForget();
}