using AdvertisingAgency.Domain;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace AdvertisingAgency.PopupModels;

public sealed partial class CampaignActionMenuPopupModel : ObservableObject
{
    [ObservableProperty] private Campaign? _campaign;

    [RelayCommand]
    private async Task DeleteCampaign()
    {
        bool answer = await App.Current!.MainPage!.DisplayAlert("Удаление",
            "Вы уверены, что хотите удалить кампанию?", "Да", "Нет");
    }
}