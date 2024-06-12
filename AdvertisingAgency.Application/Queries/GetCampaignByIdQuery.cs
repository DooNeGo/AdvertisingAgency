using AdvertisingAgency.Application.Interfaces;
using AdvertisingAgency.Domain;
using Mediator;
using Microsoft.EntityFrameworkCore;

namespace AdvertisingAgency.Application.Queries;

public sealed record GetCampaignByIdQuery(CampaignId Id) : IQuery<Campaign>;

internal sealed class GetCampaignByIdQueryHadnler(IApplicationContext context)
    : IQueryHandler<GetCampaignByIdQuery, Campaign>
{
    public ValueTask<Campaign> Handle(GetCampaignByIdQuery query, CancellationToken cancellationToken) =>
        new(context.Campaigns
            .Where(campaign => campaign.Id == query.Id)
            .Include(campaign => campaign.Settings)
            .ThenInclude(settings => settings.AdSchedules)
            .Include(campaign => campaign.Employee)
            .FirstAsync(cancellationToken));
}