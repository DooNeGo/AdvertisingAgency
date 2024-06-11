using AdvertisingAgency.Application.Interfaces;
using AdvertisingAgency.Domain;
using Mediator;
using Microsoft.EntityFrameworkCore;

namespace AdvertisingAgency.Application.Queries;

public sealed record GetCampaignTypesQuery : IQuery<IAsyncEnumerable<CampaignType>>;

internal sealed class GetCampaignTypesQueryHandler(IApplicationContext context)
    : IQueryHandler<GetCampaignTypesQuery, IAsyncEnumerable<CampaignType>>
{
    public ValueTask<IAsyncEnumerable<CampaignType>> Handle(GetCampaignTypesQuery query,
        CancellationToken cancellationToken) =>
        new(context.CampaignTypes.AsNoTracking().AsAsyncEnumerable());
}