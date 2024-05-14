using AdvertisingAgency.Application.Queries;
using AdvertisingAgency.Domain;
using CommunityToolkit.Mvvm.ComponentModel;
using Mediator;
using Location = AdvertisingAgency.Domain.Location;

namespace AdvertisingAgency.ViewModels.CreateCampaign;

public sealed partial class CampaignSettingsViewModel : ObservableObject
{
    [ObservableProperty] private List<Location> _locations = [];
    [ObservableProperty] private List<Language> _languages = [];
    
    [ObservableProperty] private List<AdSchedule> _schedules = [];

    public CampaignSettingsViewModel(IMediator mediator)
    {
        Task.Run(async () =>
        {
            Locations = await mediator.Send(new GetLocationsQuery());
            Languages = await mediator.Send(new GetLanguagesQuery());
        });
    }
}