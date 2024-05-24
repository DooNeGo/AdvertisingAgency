using AdvertisingAgency.Application.Interfaces;
using AdvertisingAgency.Domain;
using Mediator;
using Microsoft.EntityFrameworkCore;

namespace AdvertisingAgency.Application.Queries;

public sealed record GetCampaignGoalsQuery : IQuery<IAsyncEnumerable<CampaignGoal>>;

public sealed class GetCampaignGoalsQueryHandler(IApplicationContext context)
    : IQueryHandler<GetCampaignGoalsQuery, IAsyncEnumerable<CampaignGoal>>
{
    public ValueTask<IAsyncEnumerable<CampaignGoal>> Handle(GetCampaignGoalsQuery query,
        CancellationToken cancellationToken) =>
        new(context.CampaignGoals.AsNoTracking().AsAsyncEnumerable());
}