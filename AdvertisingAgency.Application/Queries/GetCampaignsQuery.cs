using AdvertisingAgency.Application.Interfaces;
using AdvertisingAgency.Domain;
using Mediator;
using Microsoft.EntityFrameworkCore;

namespace AdvertisingAgency.Application.Queries;

public sealed record GetCampaignsQuery(ClientId Id) : IQuery<IAsyncEnumerable<Campaign>>;

internal sealed class GetCampaignsQueryHandler(IApplicationContext context) : IQueryHandler<GetCampaignsQuery, IAsyncEnumerable<Campaign>>
{
    public ValueTask<IAsyncEnumerable<Campaign>> Handle(GetCampaignsQuery query, CancellationToken cancellationToken) =>
        new(context.Campaigns
            .Where(campaign => campaign.Client.Id == query.Id)
            .Include(campaign => campaign.Client)
            .Include(campaign => campaign.Employee)
            .Include(campaign => campaign.Settings)
            .AsAsyncEnumerable());
}