using AdvertisingAgency.Application.Interfaces;
using AdvertisingAgency.Domain;
using Mediator;
using Microsoft.EntityFrameworkCore;

namespace AdvertisingAgency.Application.Queries;

public sealed record GetCampaignTypesQuery : IQuery<List<CampaignType>>;

public sealed class GetCampaignTypesQueryHandler(IApplicationContext context) : IQueryHandler<GetCampaignTypesQuery, List<CampaignType>>
{
    public ValueTask<List<CampaignType>> Handle(GetCampaignTypesQuery query, CancellationToken cancellationToken) =>
        new(context.CampaignTypes.AsNoTracking().ToListAsync(cancellationToken));
}