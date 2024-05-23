using AdvertisingAgency.ViewModels.CreateCampaign;

namespace AdvertisingAgency.Views.CreateCampaign;

public sealed partial class CampaignSettingsView
{
    private readonly CampaignSettingsViewModel _viewModel;

    private double _previousBorderHeight;
    
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
        await visualElement.ScaleTo(1, 350, Easing.SpringOut);
    }

    private async void Button_OnClicked(object? sender, EventArgs e)
    {
        if (!ExpanderView.IsExpanded) return;
        if (sender is not VisualElement element) return;
        var visualElement = (VisualElement)element.Parent.Parent;
        await visualElement.FadeTo(0, easing: Easing.Default);
        
        _viewModel.DeleteScheduleCommand.Execute(visualElement.BindingContext);
    }

    private void ScheduleFrame_OnSizeChanged(object? sender, EventArgs e)
    {
        if (!ExpanderView.IsExpanded) return;
        if (_previousBorderHeight is 0) _previousBorderHeight = ScheduleFrame.Height;
        
        ScheduleFrame.Animate("ResizingFrame", d => ScheduleFrame.HeightRequest = d,
            _previousBorderHeight, ScheduleFrame.Height, easing: Easing.CubicOut);

        _previousBorderHeight = ScheduleFrame.Height;
    }
}