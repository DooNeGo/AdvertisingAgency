using AdvertisingAgency.PopupModels;
using AdvertisingAgency.Popups;
using AdvertisingAgency.ViewModels;
using AdvertisingAgency.ViewModels.CreateCampaign;
using AdvertisingAgency.Views;
using AdvertisingAgency.Views.CreateCampaign;
using CommunityToolkit.Maui;

namespace AdvertisingAgency;

internal static class DependencyInjection
{
    public static IServiceCollection AddUI(this IServiceCollection serviceCollection) =>
        serviceCollection.AddTransientWithShellRoute<LoginView, LoginViewModel>(nameof(LoginViewModel))
            .AddTransientWithShellRoute<CreateAccountView, CreateAccountViewModel>(nameof(CreateAccountViewModel))
            .AddTransientWithShellRoute<AgencyInfoView, AgencyInfoViewModel>(nameof(AgencyInfoViewModel))
            .AddTransientWithShellRoute<CampaignsView, CampaignsViewModel>(nameof(CampaignsViewModel))
            .AddTransientWithShellRoute<ChooseCampaignGoalView, ChooseCampaignGoalViewModel>(nameof(ChooseCampaignGoalViewModel))
            .AddTransientWithShellRoute<ChooseCampaignTypeView, ChooseCampaignTypeViewModel>(nameof(ChooseCampaignTypeViewModel))
            .AddTransientWithShellRoute<CampaignSettingsView, CampaignSettingsViewModel>(nameof(CampaignSettingsViewModel))
            .AddTransientWithShellRoute<UserView, UserViewModel>(nameof(UserViewModel))
            .AddTransientPopup<CampaignActionMenuPopup, CampaignActionMenuPopupModel>();
}