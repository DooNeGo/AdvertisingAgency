using AdvertisingAgency.Application.Interfaces;
using AdvertisingAgency.Views;

namespace AdvertisingAgency;

public sealed partial class App
{
    public App(IIdentityService identityService, IServiceProvider serviceProvider)
    {
        InitializeComponent();

        MainPage = serviceProvider.GetRequiredService<LoginView>();
        UserAppTheme = AppTheme.Light;

        identityService.Authorized += _ => MainPage = new AppShell();
    }
}