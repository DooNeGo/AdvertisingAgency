using AdvertisingAgency.Application.Interfaces;
using AdvertisingAgency.Domain;
using Mediator;

namespace AdvertisingAgency.Application.Commands;

public sealed record UpdateCampaignCommand(Campaign Campaign) : ICommand;

internal sealed class UpdateCampaignCommandHandler(IApplicationContext context) : ICommandHandler<UpdateCampaignCommand>
{
    public async ValueTask<Unit> Handle(UpdateCampaignCommand command, CancellationToken cancellationToken)
    {
        context.Campaigns.Update(command.Campaign);
        await context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        return Unit.Value;
    }
}
