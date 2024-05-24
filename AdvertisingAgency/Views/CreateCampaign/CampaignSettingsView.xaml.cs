using AdvertisingAgency.ViewModels.CreateCampaign;

namespace AdvertisingAgency.Views.CreateCampaign;

public sealed partial class CampaignSettingsView
{
    private readonly CampaignSettingsViewModel _viewModel;

    public CampaignSettingsView(CampaignSettingsViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        BindingContext = _viewModel;
    }

    private async void VisualElement_OnLoaded(object? sender, EventArgs e)
    {
        if (!ExpanderView.IsExpanded) return;
        if (sender is not VisualElement visualElement) return;
        visualElement.Scale = 0;
        await visualElement.ScaleTo(1, 350, Easing.Default).ConfigureAwait(false);
    }

    private async void Button_OnClicked(object? sender, EventArgs e)
    {
        if (!ExpanderView.IsExpanded) return;
        if (sender is not VisualElement element) return;
        var visualElement = (VisualElement)element.Parent.Parent;
        await visualElement.FadeTo(0, 300, Easing.Default);
        
        _viewModel.DeleteScheduleCommand.Execute(visualElement.BindingContext);
    }
}