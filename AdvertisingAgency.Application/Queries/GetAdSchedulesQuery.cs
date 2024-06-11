using AdvertisingAgency.Application.Interfaces;
using AdvertisingAgency.Domain;
using Mediator;
using Microsoft.EntityFrameworkCore;

namespace AdvertisingAgency.Application.Queries;

public sealed record GetAdSchedulesQuery(CampaignSettingsId Id) : IQuery<List<AdSchedule>>;

internal sealed class GetAdSchedulesQueryHandler(IApplicationContext context)
    : IQueryHandler<GetAdSchedulesQuery, List<AdSchedule>>
{
    public ValueTask<List<AdSchedule>> Handle(GetAdSchedulesQuery query, CancellationToken cancellationToken) =>
        new(context.CampaignSettings
            .AsNoTracking()
            .Where(settings => settings.Id == query.Id)
            .Select(settings => settings.AdSchedules)
            .FirstAsync(cancellationToken));
}