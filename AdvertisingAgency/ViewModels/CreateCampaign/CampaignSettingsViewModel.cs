using AdvertisingAgency.Application.Commands;
using AdvertisingAgency.Application.Queries;
using AdvertisingAgency.Domain;
using AdvertisingAgency.Messages;
using AsyncAwaitBestPractices;
using CommunityToolkit.Diagnostics;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Mediator;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using Location = AdvertisingAgency.Domain.Location;

namespace AdvertisingAgency.ViewModels.CreateCampaign;

public sealed partial class CampaignSettingsViewModel : ObservableValidator, IQueryAttributable
{
    [ObservableProperty]
    [NotifyDataErrorInfo]
    [Required(ErrorMessage = "Обязательное")]
    private string _name = string.Empty;

    [ObservableProperty]
    [NotifyDataErrorInfo]
    [MinLength(1, ErrorMessage = "Обязательное")]
    private List<Location> _selectedLocations = [];

    [ObservableProperty]
    [NotifyDataErrorInfo]
    [MinLength(1, ErrorMessage = "Обязательное")]
    private List<Language> _selectedLanguages = [];

    [ObservableProperty] private decimal _budget;

    [ObservableProperty] private string _campaignNameError = string.Empty;
    [ObservableProperty] private string _selectedLocationsError = string.Empty;
    [ObservableProperty] private string _selectedLanguagesError = string.Empty;

    [ObservableProperty] private List<DayOfWeek> _dayOfWeeks;
    [ObservableProperty] private List<Location> _locations = [];
    [ObservableProperty] private List<Language> _languages = [];

    [ObservableProperty]
    private ObservableCollection<AdSchedule> _schedules =
        [new AdSchedule(DayOfWeek.Monday, new DateTime(), new DateTime(DateOnly.MinValue, TimeOnly.MaxValue))];

    private readonly IMediator _mediator;
    private readonly IMessenger _messenger;

    private CampaignGoal? _campaignGoal;
    private CampaignType? _campaignType;
    private Campaign? _campaign;

    public CampaignSettingsViewModel(IMediator mediator, IMessenger messenger)
    {
        _mediator = mediator;
        _messenger = messenger;

        Task.Run(async () =>
        {
            Languages = await mediator.Send(new GetLanguagesQuery()).ConfigureAwait(false);
            Locations = await mediator.Send(new GetLocationsQuery()).ConfigureAwait(false);
        }).SafeFireAndForget();

        DayOfWeeks = [.. Enum.GetValues<DayOfWeek>()];
    }

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.TryGetValue("Campaign", out object? campaign))
        {
            _campaign = (Campaign)campaign;
            Name = _campaign.Name;
            SelectedLanguages = _campaign.Settings.ClientSpeakLanguages;
            SelectedLocations = _campaign.Settings.Locations;
            Schedules = [.._campaign.Settings.AdSchedules];
        }
        else
        {
            _campaignGoal = (CampaignGoal)query["CampaignGoal"];
            _campaignType = (CampaignType)query["CampaignType"];
        }
    }

    [RelayCommand]
    private void AddSchedule() =>
        Schedules.Add(new AdSchedule(DayOfWeek.Monday, new DateTime(), new DateTime(DateOnly.MinValue, TimeOnly.MaxValue)));

    [RelayCommand]
    private void DeleteSchedule(AdSchedule schedule) => Schedules.Remove(schedule);

    [RelayCommand]
    private async Task FinishAsync(CancellationToken cancellationToken = default)
    {
        await PrepareFinishAsync(cancellationToken).ConfigureAwait(false);
        if (HasErrors) return;

        var settings = new CampaignSettings(Budget, SelectedLocations, SelectedLanguages, [.. Schedules]);

        try
        {
            CampaignId id = await _mediator
                .Send(new AddCampaignCommand(_campaignGoal!, _campaignType!, settings, Name), cancellationToken)
                .ConfigureAwait(false);
            _messenger.Send(new AddCampaignMessage(id));
        }
        catch (Exception e)
        {
            Debug.WriteLine(e);
            await ShowUnsuccessfulAlert(cancellationToken).ConfigureAwait(false);
            return;
        }

        await ShowSuccessfulAlert(cancellationToken).ConfigureAwait(false);
        await GoBackAsync(cancellationToken).ConfigureAwait(false);
    }

    private async Task PrepareFinishAsync(CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        Guard.IsNotNull(_campaignGoal, nameof(_campaignGoal));
        Guard.IsNotNull(_campaignType, nameof(_campaignType));

        ValidateAllProperties();
        await UpdateAllErrorProperties(cancellationToken).ConfigureAwait(false);
    }

    private Task UpdateAllErrorProperties(CancellationToken cancellationToken = default) => Task.Run(() =>
    {
        CampaignNameError = GetErrorMessageOrEmpty(nameof(Name));
        SelectedLocationsError = GetErrorMessageOrEmpty(nameof(SelectedLocations));
        SelectedLanguagesError = GetErrorMessageOrEmpty(nameof(SelectedLanguages));
    }, cancellationToken);

    private string GetErrorMessageOrEmpty(string propertyName) =>
        GetErrors(propertyName).FirstOrDefault()?.ErrorMessage ?? string.Empty;

    private static Task GoBackAsync(CancellationToken cancellationToken = default) =>
        Shell.Current.GoToAsync("../../..").WaitAsync(cancellationToken);

    private static Task ShowSuccessfulAlert(CancellationToken cancellationToken = default) =>
        App.Current!.Dispatcher.DispatchAsync(async () =>
            await App.Current.MainPage!.DisplayAlert("Кампания", "Вы успешно создали новую кампанию", "Ок")
                .WaitAsync(cancellationToken));

    private static Task ShowUnsuccessfulAlert(CancellationToken cancellationToken = default) =>
        App.Current!.Dispatcher.DispatchAsync(async () =>
            await App.Current!.MainPage!.DisplayAlert("Кампания", "Ошибка. Не удалось создать кампанию", "Ок")
                .WaitAsync(cancellationToken));
}