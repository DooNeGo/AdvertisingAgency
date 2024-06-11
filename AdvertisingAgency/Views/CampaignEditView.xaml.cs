using AdvertisingAgency.ViewModels;

namespace AdvertisingAgency.Views;

public sealed partial class CampaignEditView : ContentPage
{
	public CampaignEditView(CampaignEditViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}