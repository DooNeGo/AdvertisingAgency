using AdvertisingAgency.Domain;
using CommunityToolkit.Mvvm.ComponentModel;

namespace AdvertisingAgency.PopupModels;

public sealed partial class CampaignActionMenuPopupModel : ObservableObject
{
    [ObservableProperty] private Campaign? _campaign;
}