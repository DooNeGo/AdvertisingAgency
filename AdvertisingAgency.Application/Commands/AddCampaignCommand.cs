using AdvertisingAgency.Application.Interfaces;
using AdvertisingAgency.Domain;
using Mediator;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace AdvertisingAgency.Application.Commands;

public sealed record AddCampaignCommand(Campaign Campaign) : ICommand<CampaignId>;

public sealed class AddCampaignCommandHandler(IApplicationContext context)
    : ICommandHandler<AddCampaignCommand, CampaignId>
{
    public async ValueTask<CampaignId> Handle(AddCampaignCommand command, CancellationToken cancellationToken)
    {
        EntityEntry<Campaign> entry = await context.Campaigns
            .AddAsync(command.Campaign, cancellationToken)
            .ConfigureAwait(false);
        
        await context.SaveChangesAsync(cancellationToken);
        return entry.Entity.Id;
    }
}