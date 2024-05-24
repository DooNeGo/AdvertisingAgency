using System.Collections.ObjectModel;
using AdvertisingAgency.Application.Queries;
using AdvertisingAgency.Domain;
using AsyncAwaitBestPractices;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Mediator;
using Location = AdvertisingAgency.Domain.Location;

namespace AdvertisingAgency.ViewModels.CreateCampaign;

public sealed partial class CampaignSettingsViewModel : ObservableObject
{
    [ObservableProperty] private List<Location> _locations = [];
    [ObservableProperty] private List<Language> _languages = [];
    [ObservableProperty] private List<DayOfWeek> _dayOfWeeks;

    [ObservableProperty] private ObservableCollection<AdSchedule> _schedules =
        [new AdSchedule(DayOfWeek.Monday, TimeSpan.Zero, TimeOnly.MaxValue.ToTimeSpan())];

    public CampaignSettingsViewModel(IMediator mediator)
    {
        Task.Run(async () =>
        {
            Languages = await mediator.Send(new GetLanguagesQuery()).ConfigureAwait(false);
            Locations = await mediator.Send(new GetLocationsQuery()).ConfigureAwait(false);
        }).SafeFireAndForget();
        
        DayOfWeeks = [..Enum.GetValues<DayOfWeek>()];
    }

    [RelayCommand]
    private void AddSchedule() =>
        Schedules.Add(new AdSchedule(DayOfWeek.Monday, TimeSpan.Zero, TimeOnly.MaxValue.ToTimeSpan()));

    [RelayCommand]
    private void DeleteSchedule(AdSchedule schedule) => Schedules.Remove(schedule);
}