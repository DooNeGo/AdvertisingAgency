using AdvertisingAgency.ViewModels;

namespace AdvertisingAgency.Views;

public sealed partial class CampaignsView
{
    private double _lastScrollY;
    
    public CampaignsView(CampaignsViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }

    private Task<bool> HideAddButton() => 
        AddButton.TranslateTo(0, AddButton.Height * 1.25, easing: Easing.Default);
    
    private Task<bool> ShowAddButton() =>
        AddButton.TranslateTo(0, 0, easing: Easing.SpringOut);

    private async void ScrollView_OnScrolled(object? sender, ScrolledEventArgs e)
    {
        double delta = e.ScrollY - _lastScrollY;
        
        if (delta > 0) await HideAddButton();
        else await ShowAddButton();
        
        _lastScrollY = e.ScrollY;
    }
}