using AdvertisingAgency.Application.Interfaces;
using AdvertisingAgency.Domain;
using Mediator;
using Microsoft.EntityFrameworkCore;

namespace AdvertisingAgency.Application.Commands;

public sealed record DeleteCampaignByIdCommand(CampaignId Id) : ICommand<int>;

internal sealed class DeleteCampaignByIdCommandHandler(IApplicationContext context)
    : ICommandHandler<DeleteCampaignByIdCommand, int>
{
    public ValueTask<int> Handle(DeleteCampaignByIdCommand command, CancellationToken cancellationToken) =>
        new(context.Campaigns
            .Where(campaign => campaign.Id == command.Id)
            .ExecuteDeleteAsync(cancellationToken));
}
