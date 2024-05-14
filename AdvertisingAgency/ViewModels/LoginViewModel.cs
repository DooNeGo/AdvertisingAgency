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
    private Task GoToCreateAccount() =>
        Shell.Current.GoToAsync(nameof(CreateAccountViewModel));

    [RelayCommand(CanExecute = nameof(CanExecuteLogin))]
    private async Task LogIn()
    {
        await identityService.AuthorizeAsync(UserName.Trim(), Password.Trim(), CancellationToken.None);
        if (identityService.CurrentUser is null) Error = "Неверный Логин или Пароль";
        else await Shell.Current.GoToAsync("///MainView");
    }

    private bool CanExecuteLogin() =>
        !string.IsNullOrWhiteSpace(UserName.Trim()) &&
        !string.IsNullOrWhiteSpace(Password.Trim()) &&
        UserName.Length >= 3 &&
        Password.Length >= 3;
}