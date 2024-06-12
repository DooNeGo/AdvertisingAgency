using AdvertisingAgency.Application.Interfaces;
using AdvertisingAgency.Domain;
using Mediator;
using Microsoft.EntityFrameworkCore;

namespace AdvertisingAgency.Application.Commands;

public sealed record UpdateCampaignCommand(Campaign Campaign) : ICommand;

internal sealed class UpdateCampaignCommandHandler(IApplicationContext context)
    : ICommandHandler<UpdateCampaignCommand>
{
    public async ValueTask<Unit> Handle(UpdateCampaignCommand command, CancellationToken cancellationToken)
    {
        Campaign campaign = await context.Campaigns
            .AsTracking()
            .Where(campaign => campaign.Id == command.Campaign.Id)
            .Include(campaign => campaign.Settings)
            .ThenInclude(settigns => settigns.AdSchedules)
            .FirstAsync(cancellationToken)
            .ConfigureAwait(false);

        Campaign updatedCampaign = command.Campaign;

        campaign.Name = updatedCampaign.Name;
        campaign.Goal = updatedCampaign.Goal;
        campaign.Type = updatedCampaign.Type;
        campaign.Settings = updatedCampaign.Settings;

        await context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

        return Unit.Value;
    }
}