namespace AdvertisingAgency.Domain;

public readonly record struct CampaignSettingsId(Guid Value)
{
    public static readonly CampaignSettingsId Empty;

    public static CampaignSettingsId Create() => new(Guid.NewGuid());
}

public sealed class CampaignSettings
{
    public CampaignSettings(decimal budget, List<Location> locations, List<Language> clientSpeakLanguages, List<AdSchedule> adSchedules)
    {
        Id = CampaignSettingsId.Create();
        Budget = budget;
        Locations = locations;
        ClientSpeakLanguages = clientSpeakLanguages;
        AdSchedules = adSchedules;
    }

    private CampaignSettings() { }

    public CampaignSettingsId Id { get; }

    public decimal Budget { get; set; } 

    public List<Location> Locations { get; set; } = null!;

    public List<Language> ClientSpeakLanguages { get; } = null!;

    public List<AdSchedule> AdSchedules { get; } = null!;
}