using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace AdvertisingAgency.ViewModels;

public sealed partial class LoginViewModel : ObservableObject
{
    [RelayCommand]
    private Task GoToCreateAccount()
    {
        return Shell.Current.GoToAsync(nameof(CreateAccountViewModel));
    }

    [RelayCommand]
    private Task LogIn()
    {
        return Shell.Current.GoToAsync("///MainView");
    }
}