using AdvertisingAgency.Application.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace AdvertisingAgency.ViewModels;

public sealed partial class UserViewModel(IIdentityService identityService) : ObservableObject
{
    [RelayCommand]
    private async Task LogoutAsync(CancellationToken cancellationToken)
    {
        if (await App.Current!.MainPage!.DisplayAlert("Выход", "Вы точно хотите выйти?",
                "Да", "Нет").WaitAsync(cancellationToken).ConfigureAwait(false))
        {
            identityService.Logout();
        }
    }
}