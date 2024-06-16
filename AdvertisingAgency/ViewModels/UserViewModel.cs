using AdvertisingAgency.Application.Dto;
using AdvertisingAgency.Application.Interfaces;
using AdvertisingAgency.Domain.Exceptions;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace AdvertisingAgency.ViewModels;

public sealed partial class UserViewModel(IIdentityService identityService, IDialogService dialogService)
    : ObservableObject
{
    [ObservableProperty] private UserDto _user = identityService.CurrentUser ?? throw new NotLoggedInException();

    [RelayCommand]
    private async Task LogoutAsync(CancellationToken cancellationToken)
    {
        bool answer = await dialogService
            .ShowQuestionAsync("Выход", "Вы точно хотите выйти?", cancellationToken)
            .ConfigureAwait(false);

        if (answer) identityService.Logout();
    }
}