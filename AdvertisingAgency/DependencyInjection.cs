using AdvertisingAgency.ViewModels;
using AdvertisingAgency.Views;
using CommunityToolkit.Maui;

namespace AdvertisingAgency;

internal static class DependencyInjection
{
    public static IServiceCollection AddUI(this IServiceCollection serviceCollection)
    {
        return serviceCollection.AddTransientWithShellRoute<LoginView, LoginViewModel>(nameof(LoginViewModel))
                .AddTransientWithShellRoute<MainTabbedView, MainTabbedViewModel>(nameof(MainTabbedViewModel))
                .AddTransientWithShellRoute<CreateAccountView, CreateAccountViewModel>(nameof(CreateAccountViewModel))
                .AddTransientWithShellRoute<AgencyInfoView, AgencyInfoViewModel>(nameof(AgencyInfoViewModel))
                .AddTransientWithShellRoute<CreateAccountView, CreateAccountViewModel>(nameof(CreateAccountViewModel))
                .AddTransientWithShellRoute<CreateAdRequestView, CreateAdRequestViewModel>(nameof(CreateAdRequestViewModel))
                .AddTransientWithShellRoute<RequestsView, RequestsViewModel>(nameof(RequestsViewModel));
    }
}