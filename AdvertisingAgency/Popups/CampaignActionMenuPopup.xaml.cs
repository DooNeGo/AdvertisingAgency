using AdvertisingAgency.PopupModels;
using AsyncAwaitBestPractices;

namespace AdvertisingAgency.Popups;

public enum CampaignActionMenuResult
{
    Cancel,
    Delete,
    Edit
}

public partial class CampaignActionMenuPopup
{
    private const int ClickDelay = 100;

    public CampaignActionMenuPopup(CampaignActionMenuPopupModel popupModel)
    {
        InitializeComponent();
        BindingContext = popupModel;
    }

    private async Task CloseWithResultAsync(CampaignActionMenuResult result,
        CancellationToken cancellationToken = default)
    {
        await Task.Delay(ClickDelay, cancellationToken).ConfigureAwait(false);
        await CloseAsync(result, cancellationToken).ConfigureAwait(false);
    }

    private void CancelButton_OnTapped(object? sender, EventArgs e) =>
        CloseWithResultAsync(CampaignActionMenuResult.Cancel).SafeFireAndForget();

    private void DeleteButton_OnTapped(object? sender, EventArgs e) =>
        CloseWithResultAsync(CampaignActionMenuResult.Delete).SafeFireAndForget();

    private void EditButton_OnTapped(object? sender, EventArgs e) =>
        CloseWithResultAsync(CampaignActionMenuResult.Edit).SafeFireAndForget();
}