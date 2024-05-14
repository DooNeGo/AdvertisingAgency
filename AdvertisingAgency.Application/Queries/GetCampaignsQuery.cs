using AdvertisingAgency.Application.Interfaces;
using AdvertisingAgency.Domain;
using Mediator;
using Microsoft.EntityFrameworkCore;

namespace AdvertisingAgency.Application.Queries;

public sealed record GetCampaignsQuery : IQuery<List<Campaign>>;

public sealed class GetCampaignsQueryHandler(IApplicationContext context) : IQueryHandler<GetCampaignsQuery, List<Campaign>>
{
    public ValueTask<List<Campaign>> Handle(GetCampaignsQuery query, CancellationToken cancellationToken) =>
        new(context.Campaigns.ToListAsync(cancellationToken));
}