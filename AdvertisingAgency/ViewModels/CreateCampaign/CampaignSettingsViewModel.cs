using System.Collections.ObjectModel;
using AdvertisingAgency.Application.Queries;
using AdvertisingAgency.Domain;
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
            Locations = await mediator.Send(new GetLocationsQuery());
            Languages = await mediator.Send(new GetLanguagesQuery());
        });
        DayOfWeeks = Enum.GetValues<DayOfWeek>().ToList();
    }

    [RelayCommand]
    private void AddSchedule() =>
        Schedules.Add(new AdSchedule(DayOfWeek.Monday, TimeSpan.Zero, TimeOnly.MaxValue.ToTimeSpan()));

    [RelayCommand]
    private void DeleteSchedule(AdSchedule schedule) => Schedules.Remove(schedule);
}