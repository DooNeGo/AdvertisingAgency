using AdvertisingAgency.ViewModels;

namespace AdvertisingAgency.Views;

public sealed partial class CampaignsView
{
    public CampaignsView(CampaignsViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }

    private Task<bool> HideAddButton() =>
        AddButton.TranslateTo(0, AddButton.Height * 1.25, easing: Easing.Default);

    private Task<bool> ShowAddButton() =>
        AddButton.TranslateTo(0, 0, easing: Easing.SpringOut);

    private async void ItemsView_OnScrolled(object? sender, ItemsViewScrolledEventArgs e)
    {
        if (e.VerticalDelta > 0) await HideAddButton().ConfigureAwait(false);
        else await ShowAddButton().ConfigureAwait(false);
    }
}