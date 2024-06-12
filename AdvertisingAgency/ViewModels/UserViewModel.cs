using AdvertisingAgency.Application.Interfaces;
using AdvertisingAgency.Domain;
using AdvertisingAgency.Domain.Exceptions;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace AdvertisingAgency.ViewModels;

public sealed partial class UserViewModel(IIdentityService identityService) : ObservableObject
{
    [ObservableProperty] private User _user = identityService.CurrentUser ?? throw new NotLoggedInException();

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