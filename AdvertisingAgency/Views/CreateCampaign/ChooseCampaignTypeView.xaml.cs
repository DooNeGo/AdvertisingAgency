using AdvertisingAgency.ViewModels;
using ChooseCampaignTypeViewModel = AdvertisingAgency.ViewModels.CreateCampaign.ChooseCampaignTypeViewModel;

namespace AdvertisingAgency.Views.CreateCampaign;

public sealed partial class ChooseCampaignTypeView
{
    private readonly ChooseCampaignTypeViewModel _viewModel;
    
    public ChooseCampaignTypeView(ChooseCampaignTypeViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        BindingContext = _viewModel;
    }

    private void TapGestureRecognizer_OnTapped(object? sender, TappedEventArgs e)
    {
        if (sender is not Frame frame) return;
        frame.BorderColor = (Color)App.Current.Resources["Primary"];
        _viewModel.SetCampaignTypeCommand.Execute(frame.BindingContext);
    }
}