using AdvertisingAgency.Application.Commands;
using AdvertisingAgency.Application.Queries;
using AdvertisingAgency.Domain;
using AsyncAwaitBestPractices;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Mediator;
using Location = AdvertisingAgency.Domain.Location;

namespace AdvertisingAgency.ViewModels;

public sealed partial class CreateAccountViewModel : ObservableObject
{
    private readonly IMediator _mediator;
    
    [ObservableProperty] private string _companyName = string.Empty;
    [ObservableProperty] private string _firstName = string.Empty;
    [ObservableProperty] private string _lastName = string.Empty;
    [ObservableProperty] private string _country = string.Empty;
    [ObservableProperty] private string _phoneNumber = string.Empty;
    [ObservableProperty] private string _userName = string.Empty;
    [ObservableProperty] private string _password = string.Empty;
    [ObservableProperty] private string _error = string.Empty;

    [ObservableProperty] private List<string> _locations = [];
    [ObservableProperty] private List<Language> _languages = [];

    [ObservableProperty] private Location? _selectedLocation;
    
    public CreateAccountViewModel(IMediator mediator)
    {
        _mediator = mediator;
        
        Task.Run(async () =>
        {
            Locations = (await mediator.Send(new GetLocationsQuery()).ConfigureAwait(false))
                .ConvertAll(input => input.ToString());
            Languages = await mediator.Send(new GetLanguagesQuery()).ConfigureAwait(false);
        }).SafeFireAndForget();
    }

    [RelayCommand]
    private async Task RegisterAccount(CancellationToken cancellationToken)
    {
        var client = new Client(CompanyName, PhoneNumber, new FullName(FirstName, LastName), SelectedLocation!);
        var user = new User(UserName, Password, client);
        try
        {
            await _mediator.Send(new RegisterUserCommand(user), cancellationToken).ConfigureAwait(false);
            await GoBackSuccessful().ConfigureAwait(false);
        }
        catch (Exception e)
        {
            Error = e.Message;
        }
    }

    private static Task ShowSuccessfulRegistrationAlert(Page page, CancellationToken cancellationToken) =>
        page.DisplayAlert("Успешная регистрация",
                "Вы успешно зарегистрировались. Войдите с вашим новым аккаунтом", "Ок")
            .WaitAsync(cancellationToken);

    private static Task GoBackSuccessful() =>
        App.Current.Dispatcher.DispatchAsync(async () =>
        {
            Page page = App.Current.MainPage!;
            await ShowSuccessfulRegistrationAlert(page, CancellationToken.None);
            await page.Navigation.PopAsync();
        });
}