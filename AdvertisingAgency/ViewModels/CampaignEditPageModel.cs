using AdvertisingAgency.Application.Queries;
using AdvertisingAgency.Domain;
using AdvertisingAgency.Extensions;
using CommunityToolkit.Mvvm.ComponentModel;
using Mediator;
using Location = AdvertisingAgency.Domain.Location;

namespace AdvertisingAgency.ViewModels;

public sealed partial class CampaignEditViewModel(IMediator mediator) : ObservableObject, IQueryAttributable
{
    [ObservableProperty] private List<Location> _locations = [];
    [ObservableProperty] private List<Language> _languages = [];
    [ObservableProperty] private List<CampaignGoal> _campaignGoals = [];
    [ObservableProperty] private List<CampaignType> _campaignTypes = [];

    [ObservableProperty] private Campaign? _campaign;

    public async void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        Campaign = (Campaign)query["Campaign"];

        Locations = await mediator.Send(new GetLocationsQuery()).ConfigureAwait(false);
        Languages = await mediator.Send(new GetLanguagesQuery()).ConfigureAwait(false);

        await UpdatePropertyFromAsyncEnumerableAsync(CampaignGoals, new GetCampaignGoalsQuery())
            .ConfigureAwait(false);

        await UpdatePropertyFromAsyncEnumerableAsync(CampaignTypes, new GetCampaignTypesQuery())
            .ConfigureAwait(false);
    }

    private async Task UpdatePropertyFromAsyncEnumerableAsync<T>(List<T> list, IQuery<IAsyncEnumerable<T>> query)
    {
        IAsyncEnumerable<T> enumerable = await mediator.Send(query).ConfigureAwait(false);
        await list.AddRangeFromAsyncEnumarableAsync(enumerable).ConfigureAwait(false);
    }
}