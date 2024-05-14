using AdvertisingAgency.ViewModels.CreateCampaign;

namespace AdvertisingAgency.Views.CreateCampaign;

public sealed partial class CampaignSettingsView
{
    public CampaignSettingsView(CampaignSettingsViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}