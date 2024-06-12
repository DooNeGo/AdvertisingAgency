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
        context.Campaigns.Update(command.Campaign);
        await context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        //Campaign campaignUpdate = command.Campaign;

        //await context.Campaigns
        //    .Where(campaign => campaign.Id == campaignUpdate.Id)
        //    .Select(campaign => new { Campaign = campaign, campaignUpdate.Name, campaignUpdate.Goal, campaignUpdate.Type, campaignUpdate.Settings})
        //    .ExecuteUpdateAsync(setters => setters
        //        .SetProperty(c => c.Campaign.Name, campaign => campaign.Name)
        //        .SetProperty(c => c.Campaign.Goal, campaign => campaign.Goal)
        //        .SetProperty(c => c.Campaign.Type, campaign => campaign.Type)
        //        .SetProperty(c => c.Campaign.Settings, campaign => campaign.Settings),
        //        cancellationToken).ConfigureAwait(false);

        //CampaignSettings settingsUpdate = campaignUpdate.Settings;

        //await context.CampaignSettings
        //    .Where(settings => settings.Id == command.Campaign.SettingsId)
        //    .Select(settings => new { Settings = settings, settingsUpdate.Budget, settingsUpdate.Countries, settingsUpdate.Languages, settingsUpdate.AdSchedules })
        //    .ExecuteUpdateAsync(setters => setters
        //        .SetProperty(settings => settings.Settings.Budget, settings => settings.Budget)
        //        .SetProperty(settings => settings.Settings.Languages, settings => settings.Languages)
        //        .SetProperty(settings => settings.Settings.AdSchedules, settings => settings.AdSchedules)
        //        .SetProperty(settings => settings.Settings.Countries, settings => settings.Countries),
        //        cancellationToken).ConfigureAwait(false);

        return Unit.Value;
    }
}