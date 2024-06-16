using AdvertisingAgency.Application.Commands;
using AdvertisingAgency.Application.Interfaces;
using AdvertisingAgency.Application.Queries;
using AdvertisingAgency.Converters;
using AdvertisingAgency.CustomCollections;
using AdvertisingAgency.Domain;
using AdvertisingAgency.Messages;
using CommunityToolkit.Diagnostics;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Mediator;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace AdvertisingAgency.ViewModels.CreateCampaign;

public sealed partial class CampaignSettingsViewModel : ObservableValidator, IQueryAttributable
{
    private readonly IMediator _mediator;
    private readonly IMessenger _messenger;
    private readonly IDialogService _dialogService;

    [ObservableProperty]
    [NotifyDataErrorInfo]
    [Required(ErrorMessage = "Обязательное")]
    private string _name = string.Empty;

    [ObservableProperty]
    [NotifyDataErrorInfo]
    [MinLength(1, ErrorMessage = "Обязательное")]
    private LocalizedCollection<Country, CountryToLocalizedStringConverter> _selectedCountries = [];

    [ObservableProperty]
    [NotifyDataErrorInfo]
    [MinLength(1, ErrorMessage = "Обязательное")]
    private LocalizedCollection<Language, LanguageToLocalizedStringConverter> _selectedLanguages = [];

    [ObservableProperty] private decimal _budget;

    [ObservableProperty] private string _campaignNameError = string.Empty;
    [ObservableProperty] private string _selectedCountriesError = string.Empty;
    [ObservableProperty] private string _selectedLanguagesError = string.Empty;

    [ObservableProperty] private LocalizedCollection<DayOfWeek, DayOfWeekConverter> _dayOfWeeks;
    [ObservableProperty] private LocalizedCollection<Country, CountryToLocalizedStringConverter> _countries = [];
    [ObservableProperty] private LocalizedCollection<Language, LanguageToLocalizedStringConverter> _languages = [];

    [ObservableProperty]
    private ObservableCollection<AdSchedule> _schedules =
        [new AdSchedule(DayOfWeek.Monday, new DateTime(), new DateTime(DateOnly.MinValue, TimeOnly.MaxValue))];

    private CampaignGoal? _campaignGoal;
    private CampaignType? _campaignType;
    private Campaign? _campaign;

    public CampaignSettingsViewModel(IMediator mediator, IMessenger messenger, IGlobalContext globalContext,
        IDialogService dialogService)
    {
        (_mediator, _messenger, _dialogService) = (mediator, messenger, dialogService);
        Languages = new LocalizedCollection<Language, LanguageToLocalizedStringConverter>(globalContext.Languages);
        Countries = new LocalizedCollection<Country, CountryToLocalizedStringConverter>(globalContext.Countries);
        DayOfWeeks = new LocalizedCollection<DayOfWeek, DayOfWeekConverter>(globalContext.DayOfWeeks);
    }

    public async void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.TryGetValue("Campaign", out object? obj))
        {
            _campaign = (Campaign)obj;
            CampaignSettings settings = await _mediator
                .Send(new GetCampaignSettingsByIdQuery(_campaign.Settings.Id))
                .ConfigureAwait(false);

            Name = _campaign.Name;
            Budget = settings.Budget;
            SelectedCountries.AddRange(settings.Countries);
            SelectedLanguages.AddRange(settings.Languages);
            Schedules.Clear();
            settings.AdSchedules.ForEach(Schedules.Add);
        }
        _campaignGoal = (CampaignGoal)query["CampaignGoal"];
        _campaignType = (CampaignType)query["CampaignType"];
    }

    [RelayCommand]
    private void AddSchedule() => Schedules.Add(new AdSchedule(DayOfWeek.Monday,
        new DateTime(), new DateTime(DateOnly.MinValue, TimeOnly.MaxValue)));

    [RelayCommand]
    private void DeleteSchedule(AdSchedule schedule) => Schedules.Remove(schedule);

    [RelayCommand]
    private async Task FinishAsync(CancellationToken cancellationToken = default)
    {
        await PrepareFinishAsync(cancellationToken).ConfigureAwait(false);
        if (HasErrors) return;

        try
        {
            await ExecuteCampaignFinishActionAsync(cancellationToken).ConfigureAwait(false);
            await GoBackAsync(cancellationToken).ConfigureAwait(false);
        }
        catch (Exception e)
        {
            Debug.WriteLine(e);
            await ShowErrorAlertAsync(cancellationToken).ConfigureAwait(false);
        }
    }

    private Task ExecuteCampaignFinishActionAsync(CancellationToken cancellationToken = default) =>
        _campaign switch
        {
            null => AddCampaignAsync(cancellationToken),
            _ => UpdateCampaignAsync(cancellationToken)
        };

    private async Task UpdateCampaignAsync(CancellationToken cancellationToken = default)
    {
        Guard.IsNotNull(_campaign, nameof(_campaign));

        var settings = new CampaignSettings(Budget, SelectedCountries.ToDefaultList(),
            SelectedLanguages.ToDefaultList(), [.. Schedules]);

        Campaign campaign = new Campaign(_campaign.ClientId, _campaign.EmployeeId,
            _campaignGoal!.Value, _campaignType!.Value, settings, Name) { Id = _campaign.Id };

        await _mediator.Send(new UpdateCampaignCommand(campaign), cancellationToken)
            .ConfigureAwait(false);

        await ShowSuccessfulEditAlertAsync(cancellationToken).ConfigureAwait(false);
    }

    private async Task AddCampaignAsync(CancellationToken cancellationToken = default)
    {
        var settings = new CampaignSettings(Budget, SelectedCountries.ToDefaultList(),
            SelectedLanguages.ToDefaultList(), [.. Schedules]);

        var command = new AddCampaignCommand(_campaignGoal!.Value,
            _campaignType!.Value, settings, Name);

        CampaignId id = await _mediator.Send(command, cancellationToken)
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
        SelectedCountriesError = GetErrorMessageOrEmpty(nameof(SelectedCountries));
        SelectedLanguagesError = GetErrorMessageOrEmpty(nameof(SelectedLanguages));
    }, cancellationToken);

    private string GetErrorMessageOrEmpty(string propertyName) =>
        GetErrors(propertyName).FirstOrDefault()?.ErrorMessage ?? string.Empty;

    private static Task GoBackAsync(CancellationToken cancellationToken = default) =>
        Shell.Current.GoToAsync("../../..").WaitAsync(cancellationToken);

    private Task ShowSuccessfulAddAlertAsync(CancellationToken cancellationToken = default) =>
        _dialogService.ShowInfoAsync("Кампания", "Вы успешно создали новую кампанию", cancellationToken);

    private Task ShowSuccessfulEditAlertAsync(CancellationToken cancellationToken = default) =>
        _dialogService.ShowInfoAsync("Кампания", "Вы успешно изменили кампанию", cancellationToken);

    private Task ShowErrorAlertAsync(CancellationToken cancellationToken = default) =>
        _dialogService.ShowInfoAsync("Кампания", "Ошибка", cancellationToken);
}