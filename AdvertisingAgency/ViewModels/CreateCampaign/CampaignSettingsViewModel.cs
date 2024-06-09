using System.Collections.ObjectModel;
using AdvertisingAgency.Application.Interfaces;
using AdvertisingAgency.Application.Queries;
using AdvertisingAgency.Domain;
using AdvertisingAgency.Domain.Exceptions;
using AdvertisingAgency.Messages;
using AsyncAwaitBestPractices;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Mediator;
using Location = AdvertisingAgency.Domain.Location;

namespace AdvertisingAgency.ViewModels.CreateCampaign;

public sealed partial class CampaignSettingsViewModel : ObservableObject
{
    private readonly IMediator _mediator;
    private readonly IIdentityService _identityService;
    private readonly IMessenger _messenger;
    
    [ObservableProperty] private List<Location> _locations = [];
    [ObservableProperty] private List<Location> _selectedLocations = [];
    [ObservableProperty] private List<string> _locationsString = [];
    [ObservableProperty] private List<string> _selectedLocationsString = [];
    
    [ObservableProperty] private List<Language> _languages = [];
    [ObservableProperty] private List<Language> _selectedLanguages = [];
    [ObservableProperty] private List<string> _languagesString = [];
    [ObservableProperty] private List<string> _selectedLanguagesString = [];
    
    [ObservableProperty] private List<DayOfWeek> _dayOfWeeks;

    [ObservableProperty] private ObservableCollection<AdSchedule> _schedules =
        [new AdSchedule(DayOfWeek.Monday, new DateTime(), new DateTime(DateOnly.MinValue, TimeOnly.MaxValue))];

    public CampaignSettingsViewModel(IMediator mediator, IIdentityService identityService, IMessenger messenger)
    {
        _identityService = identityService;
        _mediator = mediator;
        _messenger = messenger;
        
        Task.Run(async () =>
        {
            Languages = await mediator.Send(new GetLanguagesQuery()).ConfigureAwait(false);
            Locations = await mediator.Send(new GetLocationsQuery()).ConfigureAwait(false);
            LanguagesString = Languages.ConvertAll(language => language.ToString());
            LocationsString = Locations.ConvertAll(location => location.ToString());
        }).SafeFireAndForget();
        
        DayOfWeeks = [..Enum.GetValues<DayOfWeek>()];
    }

    [RelayCommand]
    private void AddSchedule() =>
        Schedules.Add(new AdSchedule(DayOfWeek.Monday, new DateTime(), new DateTime(DateOnly.MinValue, TimeOnly.MaxValue)));

    [RelayCommand]
    private void DeleteSchedule(AdSchedule schedule) => Schedules.Remove(schedule);

    [RelayCommand]
    private async Task FinishAsync(CancellationToken cancellationToken = default)
    {
        //Client client = _identityService.CurrentUser?.Client ?? throw new NotLoggedInException();
        //var campaign = new Campaign(client,);
        //_messenger.Send(new AddCampaignMessage(campaign.Id));
    }
}