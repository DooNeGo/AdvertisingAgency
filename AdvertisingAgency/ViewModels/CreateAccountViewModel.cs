using System.ComponentModel.DataAnnotations;
using AdvertisingAgency.Application.Commands;
using AdvertisingAgency.Application.Queries;
using AdvertisingAgency.Domain;
using AsyncAwaitBestPractices;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Mediator;
using Location = AdvertisingAgency.Domain.Location;

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
    private Location? _selectedLocation;
    
    [ObservableProperty] private string _locationError = string.Empty;
    
    [ObservableProperty]
    [NotifyDataErrorInfo]
    [Required(ErrorMessage = "Обязательное")]
    [Phone]
    [MinLength(10, ErrorMessage = "Неверная длина пароля (ожидаемая длина 13)")]
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

    [ObservableProperty] private List<Location> _locations = [];
    [ObservableProperty] private List<Language> _languages = [];
    
    public CreateAccountViewModel(IMediator mediator)
    {
        _mediator = mediator;
        
        Task.Run(async () =>
        {
            Locations = await mediator.Send(new GetLocationsQuery()).ConfigureAwait(false);
            Languages = await mediator.Send(new GetLanguagesQuery()).ConfigureAwait(false);
        }).SafeFireAndForget();
    }

    [RelayCommand]
    private async Task RegisterAccount(CancellationToken cancellationToken)
    {
        ValidateAllProperties();
        UpdateErrorProperties();
        
        if (HasErrors) return;
        
        var client = new Client(CompanyName, PhoneNumber, new FullName(FirstName, LastName), SelectedLocation!);
        var user = new User(UserName, Password, client);
        try
        {
            await _mediator.Send(new RegisterUserCommand(user), cancellationToken).ConfigureAwait(false);
            await GoBackSuccessfulAsync(cancellationToken).ConfigureAwait(false);
        }
        catch (Exception e)
        {
            Error = e.InnerException!.Message;
        }
    }

    private void UpdateErrorProperties()
    {
        CompanyNameError = GetErrors(nameof(CompanyName)).FirstOrDefault()?.ErrorMessage ?? string.Empty;
        FirstNameError = GetErrors(nameof(FirstName)).FirstOrDefault()?.ErrorMessage ?? string.Empty;
        LastNameError = GetErrors(nameof(LastName)).FirstOrDefault()?.ErrorMessage ?? string.Empty;
        LocationError = GetErrors(nameof(SelectedLocation)).FirstOrDefault()?.ErrorMessage ?? string.Empty;
        PhoneNumberError = GetErrors(nameof(PhoneNumber)).FirstOrDefault()?.ErrorMessage ?? string.Empty;
        UserNameError = GetErrors(nameof(UserName)).FirstOrDefault()?.ErrorMessage ?? string.Empty;
        PasswordError = GetErrors(nameof(Password)).FirstOrDefault()?.ErrorMessage ?? string.Empty;
    }

    private static Task ShowSuccessfulRegistrationAlert(Page page, CancellationToken cancellationToken) =>
        page.DisplayAlert("Успешная регистрация",
                "Вы успешно зарегистрировались. Войдите с вашим новым аккаунтом", "Ок")
            .WaitAsync(cancellationToken);

    private static async Task GoBackSuccessfulAsync(CancellationToken cancellationToken)
        //App.Current!.Dispatcher.DispatchAsync(async () =>
        {
            Page page = App.Current.MainPage!;
            await ShowSuccessfulRegistrationAlert(page, cancellationToken);
            await page.Navigation.PopAsync().WaitAsync(cancellationToken);
        }
}