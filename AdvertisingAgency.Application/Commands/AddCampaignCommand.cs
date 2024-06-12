using AdvertisingAgency.Application.Interfaces;
using AdvertisingAgency.Domain;
using AdvertisingAgency.Domain.Exceptions;
using Mediator;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace AdvertisingAgency.Application.Commands;

public sealed record AddCampaignCommand(CampaignGoal Goal, CampaignType Type, CampaignSettings Settings, string Name)
    : ICommand<CampaignId>;

public sealed class AddCampaignCommandHandler(IApplicationContext context, IIdentityService identityService)
    : ICommandHandler<AddCampaignCommand, CampaignId>
{
    public async ValueTask<CampaignId> Handle(AddCampaignCommand command, CancellationToken cancellationToken)
    {
        Client client = identityService.CurrentUser?.Client ?? throw new NotLoggedInException();
        Employee employee = await context.Employees.FirstAsync(cancellationToken).ConfigureAwait(false);
        Campaign campaign = new(client.Id, employee.Id, command.Goal, command.Type, command.Settings, command.Name);

        EntityEntry<Campaign> entry = await context.Campaigns
            .AddAsync(campaign, cancellationToken)
            .ConfigureAwait(false);

        await context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        return entry.Entity.Id;
    }
}