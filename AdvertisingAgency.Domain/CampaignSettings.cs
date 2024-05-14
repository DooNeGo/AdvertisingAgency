namespace AdvertisingAgency.Domain;

public readonly record struct CampaignSettingsId(Guid Value)
{
    public static readonly CampaignSettingsId Empty = new();

    public static CampaignSettingsId Create() => new(Guid.NewGuid());
}

public sealed class CampaignSettings
{
    public CampaignSettings(List<Location> locations, List<Language> clientSpeakLanguages, List<AdSchedule> adSchedules)
    {
        Id = CampaignSettingsId.Create();
        Locations = locations;
        ClientSpeakLanguages = clientSpeakLanguages;
        AdSchedules = adSchedules;
    }

    private CampaignSettings() { }

    public CampaignSettingsId Id { get; }

    public List<Location> Locations { get; set; } = null!;

    public List<Language> ClientSpeakLanguages { get; } = null!;

    public List<AdSchedule> AdSchedules { get; } = null!;
}