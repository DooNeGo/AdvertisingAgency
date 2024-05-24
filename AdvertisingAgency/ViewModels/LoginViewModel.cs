using AdvertisingAgency.Application.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace AdvertisingAgency.ViewModels;

public sealed partial class LoginViewModel(IIdentityService identityService) : ObservableObject
{
    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(LogInCommand))]
    private string _userName = string.Empty;
    
    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(LogInCommand))]
    private string _password = string.Empty;
    
    [ObservableProperty] private string _error = string.Empty;
    
    [RelayCommand]
    private Task GoToCreateAccount(CancellationToken cancellationToken) =>
        Shell.Current.GoToAsync(nameof(CreateAccountViewModel))
            .WaitAsync(cancellationToken);

    [RelayCommand(CanExecute = nameof(CanExecuteLogin))]
    private async Task LogIn(CancellationToken cancellationToken)
    {
        await identityService.AuthorizeAsync(UserName.Trim(), Password.Trim(), cancellationToken)
            .ConfigureAwait(false);

        if (identityService.CurrentUser is null)
        {
            Error = "Неверный Логин или Пароль";
        }
        else
        {
            await Shell.Current.GoToAsync("///MainView")
                .WaitAsync(cancellationToken)
                .ConfigureAwait(false);
        }
    }

    private bool CanExecuteLogin() =>
        !string.IsNullOrWhiteSpace(UserName) &&
        !string.IsNullOrWhiteSpace(Password) &&
        UserName.Trim().Length >= 3 &&
        Password.Trim().Length >= 3;
}