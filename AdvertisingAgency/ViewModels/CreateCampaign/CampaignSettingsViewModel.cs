using AdvertisingAgency.Application.Commands;
using AdvertisingAgency.Application.Queries;
using AdvertisingAgency.Domain;
using AdvertisingAgency.Extensions;
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
    private ObservableCollection<Location> _selectedLocations = [];

    [ObservableProperty]
    [NotifyDataErrorInfo]
    [MinLength(1, ErrorMessage = "Обязательное")]
    private ObservableCollection<Language> _selectedLanguages = [];

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

    public async void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.TryGetValue("Campaign", out object? obj))
        {
            _campaign = (Campaign)obj;
            _campaign.Settings = await _mediator
                .Send(new GetCampaignSettingsByIdQuery(_campaign.Settings.Id))
                .ConfigureAwait(false);

            Name = _campaign.Name;
            Budget = _campaign.Settings.Budget;
            SelectedLocations.AddRange(Locations.Where(location => _campaign.Settings.Locations.Any(l => l.Id == location.Id)));
            SelectedLanguages.AddRange(Languages.Where(language => _campaign.Settings.ClientSpeakLanguages.Any(l => l.Id == language.Id)));
            _campaign.Settings.AdSchedules.ForEach(Schedules.Add);
        }
        _campaignGoal = (CampaignGoal)query["CampaignGoal"];
        _campaignType = (CampaignType)query["CampaignType"];
    }

    [RelayCommand]
    private void AddSchedule() =>
        Schedules.Add(new AdSchedule(DayOfWeek.Monday, new DateTime(),
            new DateTime(DateOnly.MinValue, TimeOnly.MaxValue)));

    [RelayCommand]
    private void DeleteSchedule(AdSchedule schedule) => Schedules.Remove(schedule);

    [RelayCommand]
    private async Task FinishAsync(CancellationToken cancellationToken = default)
    {
        await PrepareFinishAsync(cancellationToken).ConfigureAwait(false);
        if (HasErrors) return;

        try
        {
            if (_campaign is null)
            {
                await AddCampaignAsync(cancellationToken).ConfigureAwait(false);
            }
            else
            {
                await UpdateCampaignAsync(cancellationToken).ConfigureAwait(false);
            }
        }
        catch (Exception e)
        {
            Debug.WriteLine(e);
            await ShowErrorAlertAsync(cancellationToken).ConfigureAwait(false);
            return;
        }

        await GoBackAsync(cancellationToken).ConfigureAwait(false);
    }

    private async Task UpdateCampaignAsync(CancellationToken cancellationToken = default)
    {
        Guard.IsNotNull(_campaign, nameof(_campaign));

        _campaign.Name = Name;
        _campaign.Goal = _campaignGoal!;
        _campaign.Type = _campaignType!;
        _campaign.Settings.ClientSpeakLanguages = [.. SelectedLanguages];
        _campaign.Settings.Locations = [.. SelectedLocations];
        _campaign.Settings.Budget = Budget;
        _campaign.Settings.AdSchedules = [.. Schedules];

        await _mediator.Send(new UpdateCampaignCommand(_campaign), cancellationToken)
            .ConfigureAwait(false);

        await ShowSuccessfulEditAlertAsync(cancellationToken).ConfigureAwait(false);
    }

    private async Task AddCampaignAsync(CancellationToken cancellationToken = default)
    {
        var settings = new CampaignSettings(Budget, [.. SelectedLocations], [.. SelectedLanguages], [.. Schedules]);

        CampaignId id = await _mediator
            .Send(new AddCampaignCommand(_campaignGoal!, _campaignType!, settings, Name), cancellationToken)
            .ConfigureAwait(false);
        _messenger.Send(new AddCampaignMessage(id));

        await ShowSuccessfulAddAlertAsync(cancellationToken).ConfigureAwait(false);
    }

    private Task PrepareFinishAsync(CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        Guard.IsNotNull(_campaignGoal, nameof(_campaignGoal));
        Guard.IsNotNull(_campaignType, nameof(_campaignType));

        ValidateAllProperties();
        return UpdateAllErrorProperties(cancellationToken);
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

    private static Task ShowSuccessfulAddAlertAsync(CancellationToken cancellationToken = default) =>
        ShowAlertAsync("Кампания", "Вы успешно создали новую кампанию", "Ок", cancellationToken);

    private static Task ShowSuccessfulEditAlertAsync(CancellationToken cancellationToken = default) =>
        ShowAlertAsync("Кампания", "Вы успешно изменили кампанию", "Ок", cancellationToken);

    private static Task ShowErrorAlertAsync(CancellationToken cancellationToken = default) =>
        ShowAlertAsync("Кампания", "Ошибка", "Ок", cancellationToken);

    private static Task ShowAlertAsync(string title, string message, string cancel, CancellationToken cancellationToken = default) =>
        App.Current!.Dispatcher.DispatchAsync(async () =>
            await App.Current!.MainPage!.DisplayAlert(title, message, cancel)
                .WaitAsync(cancellationToken).WaitAsync(cancellationToken));
}