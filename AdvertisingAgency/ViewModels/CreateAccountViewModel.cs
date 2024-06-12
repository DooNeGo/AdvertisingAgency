using AdvertisingAgency.Application.Commands;
using AdvertisingAgency.Domain;
using CommunityToolkit.Diagnostics;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Mediator;
using System.Collections.Immutable;
using System.ComponentModel.DataAnnotations;

namespace AdvertisingAgency.ViewModels;

public sealed partial class CreateAccountViewModel : ObservableValidator
{
    private readonly IMediator _mediator;

    [ObservableProperty]
    [NotifyDataErrorInfo]
    [Required(ErrorMessage = "Обязательное")]
    private string _companyName = string.Empty;

    [ObservableProperty] private string _companyNameError = string.Empty;

    [ObservableProperty]
    [NotifyDataErrorInfo]
    [Required(ErrorMessage = "Обязательное")]
    private string _firstName = string.Empty;

    [ObservableProperty] private string _firstNameError = string.Empty;

    [ObservableProperty]
    [NotifyDataErrorInfo]
    [Required(ErrorMessage = "Обязательное")]
    private string _lastName = string.Empty;

    [ObservableProperty] private string _lastNameError = string.Empty;

    [ObservableProperty]
    [NotifyDataErrorInfo]
    [Required(ErrorMessage = "Обязательное")]
    private Country? _selectedCountry;

    [ObservableProperty] private string _locationError = string.Empty;

    [ObservableProperty]
    [NotifyDataErrorInfo]
    [Required(ErrorMessage = "Обязательное")]
    [Phone]
    [MinLength(9, ErrorMessage = "Неверная длина пароля (ожидаемая длина 13)")]
    private string _phoneNumber = string.Empty;

    [ObservableProperty] private string _phoneNumberError = string.Empty;

    [ObservableProperty]
    [NotifyDataErrorInfo]
    [Required(ErrorMessage = "Обязательное")]
    [MinLength(6, ErrorMessage = "Минимальная длина логина 6")]
    private string _userName = string.Empty;

    [ObservableProperty] private string _userNameError = string.Empty;

    [ObservableProperty]
    [NotifyDataErrorInfo]
    [Required(ErrorMessage = "Обязательное")]
    [MinLength(6, ErrorMessage = "Минимальная длина пароля 6")]
    private string _password = string.Empty;

    [ObservableProperty] private string _passwordError = string.Empty;
    [ObservableProperty] private string _error = string.Empty;

    [ObservableProperty] private ImmutableArray<Country> _countries = [];
    [ObservableProperty] private ImmutableArray<Language> _languages = [];

    public CreateAccountViewModel(IMediator mediator, IGlobalContext globalContext)
    {
        _mediator = mediator;
        Countries = globalContext.Countries;
        Languages = globalContext.Languages;
    }

    [RelayCommand]
    private async Task RegisterAccount(CancellationToken cancellationToken)
    {
        await TrimProperties(cancellationToken).ConfigureAwait(false);
        ValidateAllProperties();
        await UpdateErrorProperties(cancellationToken).ConfigureAwait(false);

        if (HasErrors) return;
        Guard.IsNotNull(SelectedCountry, nameof(SelectedCountry));

        var client = new Client(CompanyName, PhoneNumber, new FullName(FirstName, LastName), SelectedCountry.Value);
        var user = new User(UserName, Password, client);
        try
        {
            await _mediator.Send(new RegisterUserCommand(user), cancellationToken).ConfigureAwait(false);
            await GoBackSuccessfulAsync(cancellationToken).ConfigureAwait(false);
        }
        catch (Exception e)
        {
            if (e.InnerException is not null) Error = e.InnerException.Message;
            Error = e.Message;
        }
    }

    private Task TrimProperties(CancellationToken cancellationToken = default) => Task.Run(() =>
    {
        CompanyName = CompanyName.TrimEnd();
        FirstName = FirstName.TrimEnd();
        LastName = LastName.TrimEnd();
        PhoneNumber = PhoneNumber.TrimEnd();
        UserName = UserName.TrimEnd();
        Password = Password.TrimEnd();
    }, cancellationToken);

    private Task UpdateErrorProperties(CancellationToken cancellationToken = default) => Task.Run(() =>
    {
        CompanyNameError = GetErrorMessageOrEmpty(nameof(CompanyName));
        FirstNameError = GetErrorMessageOrEmpty(nameof(FirstName));
        LastNameError = GetErrorMessageOrEmpty(nameof(LastName));
        LocationError = GetErrorMessageOrEmpty(nameof(SelectedCountry));
        PhoneNumberError = GetErrorMessageOrEmpty(nameof(PhoneNumber));
        UserNameError = GetErrorMessageOrEmpty(nameof(UserName));
        PasswordError = GetErrorMessageOrEmpty(nameof(Password));
    }, cancellationToken);

    private string GetErrorMessageOrEmpty(string propertyName) =>
        GetErrors(propertyName).FirstOrDefault()?.ErrorMessage ?? string.Empty;

    private static Task ShowSuccessfulRegistrationAlert(Page page, CancellationToken cancellationToken = default) =>
        page.DisplayAlert("Успешная регистрация",
                "Вы успешно зарегистрировались. Войдите с вашим новым аккаунтом", "Ок")
            .WaitAsync(cancellationToken);

    private static Task GoBackSuccessfulAsync(CancellationToken cancellationToken = default) =>
        App.Current!.Dispatcher.DispatchAsync(async () =>
        {
            Guard.IsNotNull(App.Current.MainPage, nameof(App.Current.MainPage));
            Page page = App.Current.MainPage;
            await ShowSuccessfulRegistrationAlert(page, cancellationToken);
            await page.Navigation.PopAsync().WaitAsync(cancellationToken);
        }).WaitAsync(cancellationToken);
}