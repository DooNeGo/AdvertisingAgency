using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace AdvertisingAgency.ViewModels;

public sealed partial class CreateAccountViewModel : ObservableObject
{
    [RelayCommand]
    private async Task RegisterAccount()
    {
        await Shell.Current.CurrentPage.DisplayAlert("Успешная регистрация",
                "Вы успешно зарегистрировались. Войдите с вашим новым аккаунтом", "Ок");
        await Shell.Current.Navigation.PopAsync();
    }
}
