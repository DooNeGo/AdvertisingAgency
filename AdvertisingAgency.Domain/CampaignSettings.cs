namespace AdvertisingAgency.Domain;

public readonly record struct CampaignSettingsId(Guid Value) : IStronglyTypedId<Guid>
{
    public static readonly CampaignSettingsId Empty = new(Guid.Empty);

    public static CampaignSettingsId Create() => new(Guid.NewGuid());
}

public sealed class CampaignSettings
{
    public CampaignSettings(decimal budget, List<Country> countries, List<Language> languages, List<AdSchedule> adSchedules)
    {
        Id = CampaignSettingsId.Create();
        Budget = budget;
        Countries = countries;
        Languages = languages;
        AdSchedules = adSchedules;
    }

    private CampaignSettings() { }

    public CampaignSettingsId Id { get; init; }

    public decimal Budget { get; set; } 

    public List<Country> Countries { get; set; } = null!;

    public List<Language> Languages { get; set; } = null!;

    public List<AdSchedule> AdSchedules { get; set; } = null!;
}