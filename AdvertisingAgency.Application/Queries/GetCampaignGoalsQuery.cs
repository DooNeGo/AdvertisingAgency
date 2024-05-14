using AdvertisingAgency.Application.Interfaces;
using AdvertisingAgency.Domain;
using Mediator;
using Microsoft.EntityFrameworkCore;

namespace AdvertisingAgency.Application.Queries;

public sealed record GetCampaignGoalsQuery : IQuery<List<CampaignGoal>>;

public sealed class GetCampaignGoalsQueryHandler(IApplicationContext context) : IQueryHandler<GetCampaignGoalsQuery, List<CampaignGoal>>
{
    public ValueTask<List<CampaignGoal>> Handle(GetCampaignGoalsQuery query, CancellationToken cancellationToken) =>
        new(context.CampaignGoals.AsNoTracking().ToListAsync(cancellationToken));
}