using System.ComponentModel.DataAnnotations;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace AdvertisingAgency.ViewModels;

public sealed partial class CreateAccountViewModel : ObservableObject
{
    [Required] public string CompanyName { get; set; } = string.Empty;

    [Required] public string FirstName { get; set; } = string.Empty;

    [Required] public string LastName { get; set; } = string.Empty;

    [Required] public string Country { get; set; } = string.Empty;

    [Required] public string City { get; set; } = string.Empty;

    [Required] public string PhoneNumber { get; set; } = string.Empty;

    [Required] public string UserName { get; set; } = string.Empty;

    [Required] public string Password { get; set; } = string.Empty;

    [RelayCommand]
    private static Task RegisterAccount(CancellationToken cancellationToken) =>
        App.Current!.MainPage!.DisplayAlert("Успешная регистрация",
                "Вы успешно зарегистрировались. Войдите с вашим новым аккаунтом", "Ок")
            .WaitAsync(cancellationToken);
}