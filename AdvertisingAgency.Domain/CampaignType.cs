namespace AdvertisingAgency.Domain;

public readonly record struct CampaignTypeId(Guid Value)
{
    public static readonly CampaignTypeId Empty = new();

    public static CampaignTypeId Create() => new(Guid.NewGuid());
}

public sealed class CampaignType
{
    public CampaignType(string title, string description)
    {
        Id = CampaignTypeId.Create();
        Title = title;
        Description = description;
    }

    private CampaignType() { }

    public CampaignTypeId Id { get; }

    public string Title { get; set; } = string.Empty;
    
    public string Description { get; set; } = string.Empty;

    public override string ToString() => Title;
}