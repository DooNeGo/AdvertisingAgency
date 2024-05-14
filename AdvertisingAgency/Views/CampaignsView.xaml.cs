using AdvertisingAgency.ViewModels;

namespace AdvertisingAgency.Views;

public sealed partial class CampaignsView
{
    private readonly CampaignsViewModel _viewModel;
    
    public CampaignsView(CampaignsViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        BindingContext = _viewModel;
    }

    private void ItemsView_OnScrolled(object? sender, ItemsViewScrolledEventArgs e)
    {
        if (e.VerticalDelta > 0) HideAddButton();
        else ShowAddButton();
    }

    private Task<bool> HideAddButton() => 
        AddButton.TranslateTo(0, AddButton.Height * 1.25, easing: Easing.Default);
    
    private Task<bool> ShowAddButton() =>
        AddButton.TranslateTo(0, 0, easing: Easing.SpringOut);

    private void AddButton_OnTapped(object? sender, EventArgs e)
    {
    }
}