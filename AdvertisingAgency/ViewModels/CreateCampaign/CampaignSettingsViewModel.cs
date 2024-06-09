using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using AdvertisingAgency.Application.Commands;
using AdvertisingAgency.Application.Interfaces;
using AdvertisingAgency.Application.Queries;
using AdvertisingAgency.Domain;
using AdvertisingAgency.Domain.Exceptions;
using AdvertisingAgency.Messages;
using AsyncAwaitBestPractices;
using CommunityToolkit.Diagnostics;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Mediator;
using Location = AdvertisingAgency.Domain.Location;

namespace AdvertisingAgency.ViewModels.CreateCampaign;

public sealed partial class CampaignSettingsViewModel : ObservableValidator, IQueryAttributable
{
    private readonly IMediator _mediator;
    private readonly IMessenger _messenger;

    private CampaignGoal? _campaignGoal;
    private CampaignType? _campaignType;
    
    [ObservableProperty]
    [NotifyDataErrorInfo]
    [MinLength(1)]
    private List<Location> _selectedLocations = [];
    
    [ObservableProperty]
    [NotifyDataErrorInfo]
    [MinLength(1)]
    private List<Language> _selectedLanguages = [];
    
    [ObservableProperty]
    [NotifyDataErrorInfo]
    [MinLength(1)]
    private List<DayOfWeek> _dayOfWeeks;
    
    [ObservableProperty]
    [NotifyDataErrorInfo]
    [Required]
    private string _name = string.Empty;
    
    [ObservableProperty] private List<Location> _locations = [];
    [ObservableProperty] private List<Language> _languages = [];
    [ObservableProperty] private decimal _budget;

    [ObservableProperty] private ObservableCollection<AdSchedule> _schedules =
        [new AdSchedule(DayOfWeek.Monday, new DateTime(), new DateTime(DateOnly.MinValue, TimeOnly.MaxValue))];

    public CampaignSettingsViewModel(IMediator mediator, IMessenger messenger)
    {
        _mediator = mediator;
        _messenger = messenger;
        
        Task.Run(async () =>
        {
            Languages = await mediator.Send(new GetLanguagesQuery()).ConfigureAwait(false);
            Locations = await mediator.Send(new GetLocationsQuery()).ConfigureAwait(false);
        }).SafeFireAndForget();
        
        DayOfWeeks = [..Enum.GetValues<DayOfWeek>()];
    }
    
    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        _campaignGoal = (CampaignGoal)query["CampaignGoal"];
        _campaignType = (CampaignType)query["CampaignType"];
    }

    [RelayCommand]
    private void AddSchedule() =>
        Schedules.Add(new AdSchedule(DayOfWeek.Monday, new DateTime(), new DateTime(DateOnly.MinValue, TimeOnly.MaxValue)));

    [RelayCommand]
    private void DeleteSchedule(AdSchedule schedule) => Schedules.Remove(schedule);

    private static Task GoBack(CancellationToken cancellationToken = default) =>
        Shell.Current.GoToAsync("../../..").WaitAsync(cancellationToken);

    private static Task ShowSuccessfulAlert(CancellationToken cancellationToken = default) =>
        App.Current.MainPage
            .DisplayAlert("Кампания", "Вы успешно создали новую кампанию", "Ок")
            .WaitAsync(cancellationToken);

    [RelayCommand]
    private async Task FinishAsync(CancellationToken cancellationToken = default)
    {
        Guard.IsNotNull(_campaignGoal, nameof(_campaignGoal));
        Guard.IsNotNull(_campaignType, nameof(_campaignType));

        var setting = new CampaignSettings(Budget, SelectedLocations, SelectedLanguages, Schedules.ToList());

        CampaignId id = await _mediator
            .Send(new AddCampaignCommand(_campaignGoal, _campaignType, setting, Name), cancellationToken)
            .ConfigureAwait(false);
        _messenger.Send(new AddCampaignMessage(id));

        await ShowSuccessfulAlert(cancellationToken).ConfigureAwait(false);
        await GoBack(cancellationToken).ConfigureAwait(false);
    }
}