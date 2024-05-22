using AdvertisingAgency.ViewModels.CreateCampaign;

namespace AdvertisingAgency.Views.CreateCampaign;

public sealed partial class ChooseCampaignGoalView
{
    private readonly ChooseCampaignGoalViewModel _viewModel;
    
    public ChooseCampaignGoalView(ChooseCampaignGoalViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        BindingContext = _viewModel;
    }

    private void TapGestureRecognizer_OnTapped(object? sender, TappedEventArgs e)
    {
        if (sender is not Frame frame) return;
        frame.BorderColor = (Color)Microsoft.Maui.Controls.Application.Current!.Resources["Primary"];
        _viewModel.SetCampaignGoalCommand.Execute(frame.BindingContext);
    }
}