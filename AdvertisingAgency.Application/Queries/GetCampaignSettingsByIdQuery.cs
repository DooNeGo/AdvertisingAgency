using AdvertisingAgency.Application.Interfaces;
using AdvertisingAgency.Domain;
using Mediator;
using Microsoft.EntityFrameworkCore;

namespace AdvertisingAgency.Application.Queries;

public sealed record GetCampaignSettingsByIdQuery(CampaignSettingsId Id) : IQuery<CampaignSettings>;

internal sealed class GetCampaignSettingsByIdQueryHandler(IApplicationContext context)
    : IQueryHandler<GetCampaignSettingsByIdQuery, CampaignSettings>
{
    public ValueTask<CampaignSettings> Handle(GetCampaignSettingsByIdQuery query, CancellationToken cancellationToken) =>
        new(context.CampaignSettings.AsNoTracking()
            .Where(settings => settings.Id == query.Id)
            .Include(settings => settings.AdSchedules)
            .Include(settings => settings.Locations)
            .Include(settings => settings.ClientSpeakLanguages)
            .FirstAsync(cancellationToken));
}
