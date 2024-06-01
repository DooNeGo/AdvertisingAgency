using AdvertisingAgency.PopupModels;
using AsyncAwaitBestPractices;

namespace AdvertisingAgency.Popups;

public partial class CampaignActionMenuPopup
{
    public CampaignActionMenuPopup(CampaignActionMenuPopupModel popupModel)
    {
        InitializeComponent();
        BindingContext = popupModel;
    }

    private void ButtonView_OnTapped(object? sender, EventArgs e) => CloseAsync().SafeFireAndForget();
}