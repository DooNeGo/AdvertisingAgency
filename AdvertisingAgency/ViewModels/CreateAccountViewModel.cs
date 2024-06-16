using AdvertisingAgency.Application.Commands;
using AdvertisingAgency.Application.Interfaces;
using AdvertisingAgency.Application.Queries;
using AdvertisingAgency.Converters;
using AdvertisingAgency.CustomCollections;
using AdvertisingAgency.Domain;
using BeautySalon.UI.Attributes;
using CommunityToolkit.Diagnostics;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Mediator;
using System.ComponentModel.DataAnnotations;

namespace AdvertisingAgency.ViewModels;

public sealed partial class CreateAccountViewModel : ObservableValidator
{
    private readonly IMediator _mediator;
    private readonly IDialogService _dialogService;

    [ObservableProperty]
    [NotifyDataErrorInfo]
    [Required(ErrorMessage = "Обязательное")]
    private string _companyName = string.Empty;

    [ObservableProperty]
    [NotifyDataErrorInfo]
    [Required(ErrorMessage = "Обязательное")]
    private string _firstName = string.Empty;

    [ObservableProperty]
    [NotifyDataErrorInfo]
    [Required(ErrorMessage = "Обязательное")]
    private string _lastName = string.Empty;

    [ObservableProperty]
    [NotifyDataErrorInfo]
    [Required(ErrorMessage = "Обязательное")]
    private Country? _selectedCountry;

    [ObservableProperty]
    [NotifyDataErrorInfo]
    [Required(ErrorMessage = "Обязательное")]
    [Phone]
    [MinLength(9, ErrorMessage = "Неверная длина пароля (ожидаемая длина 13)")]
    private string _phoneNumber = string.Empty;

    [ObservableProperty]
    [NotifyDataErrorInfo]
    [Required(ErrorMessage = "Обязательное")]
    [MinLength(6, ErrorMessage = "Минимальная длина логина 6")]
    [LatinOnly("Поле должно содержать только латинские буквы, цифры и _")]
    private string _userName = string.Empty;

    [ObservableProperty]
    [NotifyDataErrorInfo]
    [Required(ErrorMessage = "Обязательное")]
    [MinLength(6, ErrorMessage = "Минимальная длина пароля 6")]
    [LatinOnly("Поле должно содержать только латинские буквы, цифры и _")]
    private string _password = string.Empty;

    [ObservableProperty] private string _companyNameError = string.Empty;
    [ObservableProperty] private string _firstNameError = string.Empty;
    [ObservableProperty] private string _lastNameError = string.Empty;
    [ObservableProperty] private string _userNameError = string.Empty;
    [ObservableProperty] private string _passwordError = string.Empty;
    [ObservableProperty] private string _phoneNumberError = string.Empty;
    [ObservableProperty] private string _locationError = string.Empty;
    [ObservableProperty] private string _error = string.Empty;

    [ObservableProperty] private LocalizedCollection<Country, CountryToLocalizedStringConverter> _countries = [];

    public CreateAccountViewModel(IMediator mediator, IGlobalContext globalContext, IDialogService dialogService)
    {
        (_mediator, _dialogService) = (mediator, dialogService);
        Countries = new LocalizedCollection<Country, CountryToLocalizedStringConverter>(globalContext.Countries);
    }

    [RelayCommand]
    private async Task RegisterAccount(CancellationToken cancellationToken)
    {
        TrimProperties();
        ValidateAllProperties();
        await UpdateErrorProperties(cancellationToken).ConfigureAwait(false);

        if (HasErrors) return;
        if (PhoneNumberError != string.Empty) return;
        if (UserNameError != string.Empty) return;

        Guard.IsNotNull(SelectedCountry, nameof(SelectedCountry));

        var client = new Client(CompanyName, PhoneNumber,
            new FullName(FirstName, LastName), SelectedCountry.Value);

        var user = new User(UserName, Password, client);

        try
        {
            await _mediator.Send(new RegisterUserCommand(user), cancellationToken)
                .ConfigureAwait(false);

            await ShowSuccessfulRegistrationAlertAsync(cancellationToken)
                .ConfigureAwait(false);

            await GoBackAsync(cancellationToken).ConfigureAwait(false);
        }
        catch
        {
            await ShowErrorRegistrationAlertAsync(cancellationToken)
                .ConfigureAwait(false);
        }
    }

    private void TrimProperties()
    {
        CompanyName = CompanyName.TrimEnd();
        FirstName = FirstName.TrimEnd();
        LastName = LastName.TrimEnd();
        PhoneNumber = PhoneNumber.TrimEnd();
        UserName = UserName.TrimEnd();
        Password = Password.TrimEnd();
    }

    private Task UpdateErrorProperties(CancellationToken cancellationToken = default) => Task.Run(async () =>
    {
        CompanyNameError = GetErrorMessageOrEmpty(nameof(CompanyName));
        FirstNameError = GetErrorMessageOrEmpty(nameof(FirstName));
        LastNameError = GetErrorMessageOrEmpty(nameof(LastName));
        LocationError = GetErrorMessageOrEmpty(nameof(SelectedCountry));
        PhoneNumberError = GetErrorMessageOrEmpty(nameof(PhoneNumber));
        UserNameError = GetErrorMessageOrEmpty(nameof(UserName));
        PasswordError = GetErrorMessageOrEmpty(nameof(Password));

        if (PhoneNumberError == string.Empty)
        {
            await CheckPhoneNumberUniquenessAsync(cancellationToken)
                .ConfigureAwait(false);
        }

        if (UserNameError == string.Empty)
        {
            await CheckUserNameUniquenessAsync(cancellationToken)
                .ConfigureAwait(false);
        }
    }, cancellationToken);

    private async Task CheckPhoneNumberUniquenessAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            bool result = await _mediator
                .Send(new IsPhoneNumberExistsQuery(PhoneNumber), cancellationToken)
                .ConfigureAwait(false);

            if (result) PhoneNumberError = "Данный номер телефона уже занят";
        }
        catch
        {
            PhoneNumberError = "Не удалось проверить уникальноcть номера телефона";
        }
    }

    private async Task CheckUserNameUniquenessAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            bool result = await _mediator
                .Send(new IsUserNameExistsQuery(UserName), cancellationToken)
                .ConfigureAwait(false);

            if (result) UserNameError = "Данный логин уже занят";
        }
        catch
        {
            UserNameError = "Не удалось проверить уникальноcть логина";
        }
    }

    private string GetErrorMessageOrEmpty(string propertyName) =>
        GetErrors(propertyName).FirstOrDefault()?.ErrorMessage ?? string.Empty;

    private Task ShowSuccessfulRegistrationAlertAsync(CancellationToken cancellationToken = default) =>
        _dialogService.ShowInfoAsync("Регистрация", "Вы успешно зарегистрировались", cancellationToken);

    private Task ShowErrorRegistrationAlertAsync(CancellationToken cancellationToken = default) =>
        _dialogService.ShowInfoAsync("Регистрация", "Ошибка", cancellationToken);

    private static Task<Page> GoBackAsync(CancellationToken cancellationToken = default) =>
        App.Current!.Dispatcher.DispatchAsync(() =>
        {
            return App.Current.MainPage!.Navigation
                .PopAsync(true)
                .WaitAsync(cancellationToken);
        });
}