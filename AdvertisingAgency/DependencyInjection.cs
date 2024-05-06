using AdvertisingAgency.ViewModels;
using AdvertisingAgency.Views;
using CommunityToolkit.Maui;

namespace AdvertisingAgency;

internal static class DependencyInjection
{
    public static IServiceCollection AddUI(this IServiceCollection serviceCollection)
    {
        return serviceCollection
            .AddTransientWithShellRoute<LoginView, LoginViewModel>(nameof(LoginViewModel))
            .AddTransientWithShellRoute<CreateAccountView, CreateAccountViewModel>(nameof(CreateAccountViewModel));
    }
}