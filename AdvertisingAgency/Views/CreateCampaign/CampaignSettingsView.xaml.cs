using AdvertisingAgency.ViewModels.CreateCampaign;

namespace AdvertisingAgency.Views.CreateCampaign;

public sealed partial class CampaignSettingsView
{
    private readonly CampaignSettingsViewModel _viewModel;

    public CampaignSettingsView(CampaignSettingsViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = _viewModel = viewModel;
    }

    private async void VisualElement_OnLoaded(object? sender, EventArgs e)
    {
        if (sender is not VisualElement visualElement) return;
        visualElement.Scale = 0;
        await visualElement.ScaleTo(1, 400, Easing.SpringOut).ConfigureAwait(false);
    }

    private async void MenuItem_OnClicked(object? sender, EventArgs e)
    {
        if (sender is not Element element) return;
        var visualElement = (VisualElement)element.Parent.Parent.Parent;
        await visualElement.FadeTo(0, 300, Easing.Default);
        _viewModel.DeleteScheduleCommand.Execute(visualElement.BindingContext);
    }
}